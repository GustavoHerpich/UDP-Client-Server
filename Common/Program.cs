using UDP_Client_Server.Common.Client;
using UDP_Client_Server.Common.Server;

namespace UDP_Client_Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3 || !int.TryParse(args[1], out int port))
            {
                Console.WriteLine("Incorrect command!");
                Console.WriteLine("Usage: <IP> <Port> <server/client>");
                return;
            }

            string ip = args[0];
            string mode = args[2].ToLower();

            switch (mode)
            {
                case "client":
                    Client.RunClient(ip, port);
                    break;
                case "server":
                    Server.RunServer(ip, port);
                    break;
                default:
                    Console.WriteLine("Invalid mode. Use 'server' or 'client'.");
                    break;
            }
        }
    }
}
