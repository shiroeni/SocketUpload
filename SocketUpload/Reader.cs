using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketUpload
{
    class Reader
    {
        private static List<Thread> ThreadList = new List<Thread>();
        private static readonly object threadsLock = new object();

        public static void StartRead(TcpClient client)
        {
            var threadStarter = new ThreadStart(
                    () => new Reader(client).Read()
                );
            var thread = new Thread(threadStarter) { IsBackground = true };

            ThreadList.Add(thread);
            thread.Start();
        }

        public Reader(TcpClient client)
        {
            Client = client;
        }

        private TcpClient Client;

        public int Read()
        {
            var run = true;
            while(run)
            {
                var buffer = new byte[1024];
                Client.GetStream().Read(buffer, 0, buffer.Length);

                var data = Encoding.ASCII.GetString(buffer);

                Console.WriteLine("Appended!");
                // TODO: put to db or broker
                File.AppendText("D:\\Sandbox\\new.txt");

                Client.GetStream().Write(Encoding.ASCII.GetBytes("[ok]"));

                if (data == "[end]")
                    run = false;
            }

            return 0; // success!
        }
    }
}
