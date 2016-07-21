using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpMessage
{
    public delegate void ConnectionEventHandler(Connection connection);
    public delegate void MessageEventHandler(Connection connection, Message message);

    public class Server : IDisposable
    {
        private readonly TcpListener _listener;

        public event ConnectionEventHandler OnConnection = delegate { };

        public Server(int port) : this(IPAddress.Any, port)
        {
        }

        public Server(IPAddress address, int port)
        {
            this._listener = new TcpListener(address, port);
            this._listener.Start();

            new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        var client = this._listener.AcceptTcpClient();
                        this.OnConnection(new Connection(client));
                    }
                }
                catch
                {

                }
            })
            { IsBackground = true, Name = "CompactComm Server" }.Start();
        }

        public void Dispose()
        {
            this._listener.Stop();
        }
    }
}
