using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketUpload
{
    class Server
    {
        private readonly TcpListener Listener;
        private List<Thread> ThreadList;

        public Server(string port)
        {
            Listener = new TcpListener(IPAddress.Parse("0.0.0.0"), int.Parse(port));
        }

        public void Run()
        {
            try
            {
                Listener.Start();

                while(true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    var clientTask = Listener.AcceptTcpClientAsync();
                    var result = clientTask.Result;

                    if (result == null)
                        return;

                    Console.WriteLine("Connected! Start receiving a data...");
                    Reader.StartRead(result);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("*** Socket Error: ${0}", e);
            }
        }
    }
}
