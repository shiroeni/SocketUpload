using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SocketUploadClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TcpClient();
            client.Connect("127.0.0.1", 4567);

            var streamReader = new StreamReader(
                    new FileStream(
                            "D:\\Sandbox\\some.txt", 
                            FileMode.Open, 
                            FileAccess.Read
                        ), 
                    Encoding.UTF8
                );

            var run = true;
            while(run)
            {
                var line = streamReader.ReadLine();

                if (line != null)
                {
                    var buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, buffer.Length);

                    var data = Encoding.ASCII.GetString(buffer);
                    
                    switch(data)
                    {
                        case "[ok]":
                            client.GetStream().Write(Encoding.ASCII.GetBytes(line));
                            break;
                        case "[stop]":
                            client.GetStream().Write(Encoding.ASCII.GetBytes("[end]"));
                            run = false;
                            break;
                    }
                    
                } 
                else
                {
                    client.GetStream().Write(Encoding.ASCII.GetBytes("[end]"));

                    run = false;
                }
            }
        }
    }
}
