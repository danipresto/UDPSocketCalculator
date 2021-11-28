
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketCalc

{
    class Program
    {
        static void Main(string[] args)
        {

            String LocalIp = IPAddress.Loopback.ToString();
            UdpClient udpClient = new UdpClient();
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            using (var process1 = new Process())
            {
                process1.StartInfo.FileName = @"C:\Users\Niggurath\source\repos\Calc_Socket_Test\CalcServer\bin\Debug\netcoreapp3.1\CalcServer.exe";
                process1.Start();
            }


            while (true) {

                Console.WriteLine("Entre a operação no formato: \n Operando1 Operação Operando2");
                string input = Console.ReadLine();

                //Preparando bytes para envio 
                Byte[] sendBytes = Encoding.ASCII.GetBytes(input);
                // Enviando os bytes para o Server/Calculadora
                try
                {
                    
                    udpClient.Send(sendBytes, sendBytes.Length, LocalIp, 11000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                // Se o usuario digitar "esc" , o laço é quebrado e o programa encerrado 
                if (input.Equals("esc")) {
                    break;
                }
                // recebendo o resultado
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);


                Console.WriteLine("Resposta = " + Encoding.ASCII.GetString(receiveBytes).ToString());


            }































            /*
            UDPSocket s = new UDPSocket();
            s.Server("127.0.0.1", 27000);

            UDPSocket c = new UDPSocket();
            c.Client("127.0.0.1", 27000);


            while (true)
            {
                Console.WriteLine("Digite um numero");
                c.Send(Console.ReadLine());
               // Console.WriteLine("Digite outro numero");
               // c.Send(Console.ReadLine());
              //  Console.WriteLine("Digite a operação");
              //  c.Send(Console.ReadLine());


            }




            */
        }
    }

}
