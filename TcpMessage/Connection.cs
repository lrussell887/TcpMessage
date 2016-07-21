using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using ProtoBuf;

namespace TcpMessage
{
    public class Connection : IDisposable
    {
        public event MessageEventHandler OnMessage = delegate { };
        public event ConnectionEventHandler OnDisconnect = delegate { };

        private readonly TcpClient _client;
        private readonly Stream _stream;
        public EndPoint EndPoint { get; private set; }

        public Connection(string hostname, int port) : this(new TcpClient(hostname, port))
        {

        }

        public Connection(TcpClient client) : this(client, client.GetStream())
        {

        }

        public Connection(TcpClient client, Stream stream)
        {
            this._client = client;
            this._stream = stream;
            this.EndPoint = this._client.Client.RemoteEndPoint;

            new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        var msg = this.GetMessage();
                        this.OnMessage(this, msg);
                    }
                }
                catch
                {
                    this.Disconnect();
                    this.OnDisconnect(this);
                }
            })
            { IsBackground = true, Name = "TcpMessage Thread" }.Start();
        }

        private Message GetMessage()
        {
            var args = Serializer.DeserializeWithLengthPrefix<MessageItem[]>(this._stream, PrefixStyle.Base128);
            if (args == null) throw new InvalidOperationException();

            return new Message(args);
        }

        public void Send(string type, params object[] args)
        {
            this.Send(new Message(type, args));
        }

        public void Send(Message m)
        {
            var args = m.ToMessageItems();

            try
            {
                Serializer.SerializeWithLengthPrefix(this._stream, args, PrefixStyle.Base128);
            }
            catch
            {
                this.Disconnect();
            }
        }

        public void Disconnect()
        {
            this._stream.Dispose();
            this._client.Close();
        }

        void IDisposable.Dispose()
        {
            this.Disconnect();
        }
    }
}
