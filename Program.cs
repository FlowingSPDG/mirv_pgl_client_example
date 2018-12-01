using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLAEServer;


namespace ConsoleApp1
{
    public class Program
    {
        static int number = 0;
        static HLAE_WS_Server HLAE_Server;
        static HLAE_WS_Client HLAE_Client;
        static bool connected;
        static bool available;
        static void Main(string[] args)
        {
            //int number = 0;

            HLAE_Server = new HLAE_WS_Server(true, "HLAE_SERVER", "127.0.0.1", 30000);
            //HLAE_Client = null;
            WAITFORCLIENT();
            while (number != 0)
            {
                Console.WriteLine("number = " + number);
                UPDATE_DATA();
            }
            Console.ReadKey();

            //HLAE_WS_Server = new HLAE_WS_Server(true, "HLAE_SERVER", "127.0.0.1", 30000);

            // Console.WriteLine("Press any key to exit.");
            // Console.ReadKey();

            void WAITFORCLIENT() // can be replaced with vpod Update()
            {
                Console.WriteLine("Waiting for client connects");
                while (!HLAE_Server.ClientAvailable())
                {
                    // while there is no client connecting we'll simply wait.
                    // UPDATE_DATA();
                    // Console.WriteLine("no client!");
                }
                while (HLAE_Server.ClientAvailable())
                {
                    CLIENT_CONNECT();
                }
            }

            void CLIENT_CONNECT()
            {
                // we got a client! connect them
                Console.WriteLine("we got a client! connect them");
                HLAE_Client = new HLAE_WS_Client("HLAE_CLIENT", HLAE_Server);
                number++;
                connected = HLAE_Client.Connected();
                //available = HLAE_Server.ClientAvailable();
                //HLAE_Client.Dispose();
                if (HLAE_Client.Connected())
                {
                    Console.WriteLine("CLIENT_CONNECT");
                    HLAE_Client.SendDataWS("echo Hello from C# !");
                     HLAE_Client.SendDataWS("mirv_pgl datastart");
                    Console.WriteLine("current client number is :" + number);
                    // HLAE_Client.Name("CLIENT_" + number);
                }
            }

            void UPDATE_DATA() // can be replaced with void Update()
            {
                // bool connected = HLAE_Client.Connected();
                
                Console.WriteLine(connected);
                //Console.WriteLine(available);
                if (connected)
                {
                    while (connected == true) // 接続時のループ処理
                    {
                        HLAE_Client.ReadDataWS();
                        connected = HLAE_Client.Connected();
                    }// HLAE_Client.SendDataWS("exec echo test");
                }
                else // 切断時のループ処理
                {
                    number--;
                    Console.WriteLine("client disconnect??");
                    Console.WriteLine(number);
                    return;
                }
            }
        }

    }
}
