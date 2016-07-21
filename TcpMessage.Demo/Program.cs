using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpMessage.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server(1234);
            server.OnConnection += Server_OnConnection;

            var client = new Connection("localhost", 1234);
            client.OnDisconnect += Client_OnDisconnect;
            client.OnMessage += Client_OnMessage;

            Console.ReadKey();
        }

        private static void Server_OnConnection(Connection connection)
        {
            connection.Send("hello", "Hi client, I see that you're connected.");
            connection.OnMessage += Connection_OnMessage;
        }

        private static void Connection_OnMessage(Connection connection, Message message)
        {
            if (message.Type == "thanks")
            {
                Console.WriteLine("CLIENT > " + message.Get<string>(0));
                connection.Send("goodbye", "No problem, see you later client!");
            }
        }

        private static void Client_OnMessage(Connection connection, Message message)
        {
            if (message.Type == "hello")
            {
                Console.WriteLine("SERVER > " + message.Get<string>(0));
                connection.Send("thanks", "Hey server, thanks for telling me!");
            }
            else if (message.Type == "goodbye")
            {
                Console.WriteLine("SERVER > " + message.Get<string>(0));
                connection.Disconnect();
            }
        }

        private static void Client_OnDisconnect(Connection connection)
        {
            Console.WriteLine("CLIENT > Disconnected from server.");
        }
    }
}
