using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Client_Server.Common.Server
{
    public static class Server
    {
        private static readonly ConcurrentQueue<Message> messageQueue = new();
        private static CancellationTokenSource? cancellationTokenSource;

        public static void RunServer(string ip, int port)
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            using var server = new UdpClient(endPoint);
            Console.WriteLine($"Server running on {ip}:{port}");

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            Task.Run(() => ProcessMessages(token), token);

            while (true)
            {
                try
                {
                    var remoteEP = new IPEndPoint(IPAddress.Any, port);
                    var data = server.Receive(ref remoteEP);

                    var messageText = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"Received from {remoteEP}: {messageText}");

                    var message = new Message
                    {
                        Data = ProcessData(messageText),
                        Address = remoteEP
                    };

                    messageQueue.Enqueue(message);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Server stopping.");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        private static async Task ProcessMessages(CancellationToken token)
        {
            using var client = new UdpClient();
            while (!token.IsCancellationRequested)
            {
                if (messageQueue.TryDequeue(out var message))
                {
                    try
                    {
                        await client.SendAsync(message.Data, message.Data.Length, message.Address);
                        Console.WriteLine($"Sent to {message.Address}: {Encoding.UTF8.GetString(message.Data)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    await Task.Delay(100, token);
                }
            }
        }
        private static byte[] ProcessData(string data)
        {
            if (data.StartsWith("file:"))
            {
                var path = data.Substring(5).Trim();
                var fileData = FileServer.GetFile(path);
                return Encoding.UTF8.GetBytes(fileData ?? "File not found.");
            }

            return Encoding.UTF8.GetBytes($"{data.ToUpper()} - [Processed by Server]");
        }
        public static void StopServer()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}