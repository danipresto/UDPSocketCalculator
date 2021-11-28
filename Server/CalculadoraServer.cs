﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CalculadoraServer
    {

        public const int PORT = 9000;

        private Socket _socket;
        private EndPoint _ep;

        private byte[] _buffer_recv;
        private ArraySegment<byte> _buffer_recv_segment;

        public void Initialize()
        {
            _buffer_recv = new byte[4096];
            _buffer_recv_segment = new(_buffer_recv);

            _ep = new IPEndPoint(IPAddress.Any, PORT);

            _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            _socket.Bind(_ep);
        }

        public void StartCalcLoop()
        {
            _ = Task.Run(async () =>
            {
                SocketReceiveMessageFromResult res;
                while (true)
                {
                    res = await _socket.ReceiveMessageFromAsync(_buffer_recv_segment, SocketFlags.None, _ep);
                    
                    await SendToC(res.RemoteEndPoint, Encoding.UTF8.GetBytes("Hello back!"));
                }   
            });
        }

        public async Task SendToC(EndPoint recipient, byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            await _socket.SendToAsync(s, SocketFlags.None, recipient);
        }


    }
}
