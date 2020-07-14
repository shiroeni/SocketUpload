using System;

namespace SocketUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server("4567");

            server.Run();
        }
    }
}
