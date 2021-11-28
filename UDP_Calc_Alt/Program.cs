using System;

namespace UDP_Calc_Alt
{
    class Program
    {
        static void Main(string[] args)
        {

            UDPServerSocket server = new UDPServerSocket();
            server.Initialize("127.0.0.1", 27000);

            UDPClientSocket client = new UDPClientSocket();
            client.Initialize("127.0.0.1", 27000);

            while (true)
            {
                Console.WriteLine("Entre a operação no formato: \n Operando1 Operação Operando2 \n Digite esc para sair");
                string input = Console.ReadLine();
                if (input == "esc") {
                    break;
                }
                client.Send(input);
                Console.ReadKey();


            }

        }
    }
}
