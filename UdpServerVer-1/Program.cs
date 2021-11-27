using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace UdpServerVer_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Server cod
            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                var bufer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(bufer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(bufer));
                }
                while (udpSocket.Available > 0);

                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение отправлено"), senderEndPoint);

                Console.WriteLine(data);

                //выход из цикла!
            }
            //звкрытие сокета!
        }
    }
}
