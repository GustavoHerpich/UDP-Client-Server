namespace UDP_Client_Server.Common.Server
{
    public static class FileServer
    {
        private static readonly Dictionary<string, string> fileSystem = new Dictionary<string, string>
        {
            { "/images/image1.png", "TmV2ZXIgZ29ubmEgZ2l2ZSB5b3UgdXA=" },
            { "/notes.txt", "Here are some important notes." },
            { "/status.txt", "System is operational." }
        };

        public static string GetFile(string path)
        {
            return fileSystem.TryGetValue(path, out var data) ? data ?? "File content is null." : "File not found.";
        }
    }
}
