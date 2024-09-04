using System.Net;

namespace UDP_Client_Server.Common
{
    public class Message
    {
        public required byte[] Data { get; set; }
        public required IPEndPoint Address { get; set; }
    }
}
