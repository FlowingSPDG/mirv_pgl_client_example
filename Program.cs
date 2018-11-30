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
        static void Main(string[] args)
        {

            HLAE_WS_Server HLAE_Server;
            HLAE_WS_Client HLAE_Client;

            HLAE_Server = new HLAE_WS_Server(true, "HLAE_SERVER", "192.168.1.14", 3000);
            WAITFORCLIENT();
            //CLIENT_CONNECT();

            //HLAE_WS_Server = new HLAE_WS_Server(true, "HLAE_SERVER", "127.0.0.1", 30000);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            void WAITFORCLIENT()
            {

                while (!HLAE_Server.ClientAvailable())
                {
                    // while there is no client connecting we'll simply wait.
                    // UPDATE_DATA();
                    // Console.WriteLine("no client!");

                }
                while (HLAE_Server.ClientAvailable())
                {
                    // while there is no client connecting we'll simply wait.
                    // UPDATE_DATA();
                    CLIENT_CONNECT();
                    Console.WriteLine("Hello!");
                    int i = 1;
                    while (i == 1)
                    {
                        UPDATE_DATA();
                    }
                }

            }

            void CLIENT_CONNECT()
            {
                // we got a client! connect them
                HLAE_Client = new HLAE_WS_Client("HLAE_CLIENT", HLAE_Server);
                if (HLAE_Client.Connected())
                {
                    Console.WriteLine("CLIENT_CONNECT");
                    {
                        HLAE_Client.SendDataWS("echo Hello from C# !");
                        HLAE_Client.SendDataWS("mirv_pgl datastart");
                    }
                }
            }

            void UPDATE_DATA()
            {
                //HLAE_Client.SendDataWS("exec echo test");
                HLAE_Client.ReadDataWS();
                //Console.WriteLine("Version : " + HLAE_Client.HLAE_Version);
                //Console.WriteLine("FOV : " + HLAE_Client.HLAE_fov);
            }
        }

    }
}
