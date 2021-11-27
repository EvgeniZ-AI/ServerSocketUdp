using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace udpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //client code
            const string ip = "127.0.0.1";//create ip
            const int port = 8082;//create port

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);//endPoint

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//socket
            udpSocket.Bind(udpEndPoint);//bind

            while (true)
            {
                Console.WriteLine("Введите сообщение");
                var messeg = Console.ReadLine();// create messege

                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);//connection
                udpSocket.SendTo(Encoding.UTF8.GetBytes(messeg), serverEndPoint);

                var bufer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);

                do
                {
                    size = udpSocket.ReceiveFrom(bufer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(bufer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
            }
        }
    }
}
