using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Client_Server.Common.Client
{
    public static class Client
    {
        public static void RunClient(string ip, int port)
        {
            using UdpClient client = new();
            Console.WriteLine("Enter your messages (type 'exit' to quit):");

            while (true)
            {
                var message = Console.ReadLine();

                if (string.IsNullOrEmpty(message)
                    || message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                try
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    client.Send(messageBytes, messageBytes.Length, ip, port);

                    IPEndPoint remoteEP = new(IPAddress.Any, port);
                    byte[] receivedData = client.Receive(ref remoteEP);

                    Console.WriteLine("Received: " + Encoding.UTF8.GetString(receivedData));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
