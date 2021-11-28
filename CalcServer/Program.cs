using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CalcServer
{
    class Program
    {
        static void Main(string[] args)
        {

            String LocalIp = IPAddress.Loopback.ToString();
            Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpClient udpServer = new UdpClient();
            

            _server.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.ReuseAddress,true);
            _server.Bind(new IPEndPoint(IPAddress.Parse(LocalIp), 11000));
            
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            

            while (true) {
                
                Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                string data = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine(data.ToString());




            }

        }
    }
}
