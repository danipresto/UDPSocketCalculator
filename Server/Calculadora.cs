using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Calculadora
    {
        public void Start(int port = 9000) 
        {

            var endPoint = new IPEndPoint( IPAddress.Loopback, port);
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Udp);
            socket.Bind(endPoint);
            socket.Listen(128);

        
        
        }

    }
}
