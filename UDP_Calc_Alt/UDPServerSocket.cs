using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Calc_Alt
{
    public class UDPServerSocket
    {
        private Socket _Serversocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        int result;
        bool flag_c = false;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Initialize(string address, int port)
        {
            _Serversocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _Serversocket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();
        }

        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);

            if (flag_c == false)
            {
                _Serversocket.Connect(epFrom);
                flag_c = true;
            }


            _Serversocket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _Serversocket.EndSend(ar);
            }, state);
        }

        private void Receive()
        {
            _Serversocket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _Serversocket.EndReceiveFrom(ar, ref epFrom);
                _Serversocket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);


                string[] operators = Encoding.ASCII.GetString(so.buffer, 0, bytes).Split(' ');


                switch (operators[1])
                {
                    case "+":
                        result = int.Parse(operators[0]) + int.Parse(operators[2]);
                        break;
                    case "-":
                        result = int.Parse(operators[0]) - int.Parse(operators[2]);
                        break;
                    case "*":
                        result = int.Parse(operators[0]) * int.Parse(operators[2]);
                        break;
                    case "/":
                        result = int.Parse(operators[0]) / int.Parse(operators[2]);
                        break;
                    default:
                        Console.WriteLine("Insira uma operação válida");
                        break;
                }


                Send("Resultado: " + result);


            }, state);
        }
    }
}
