using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace C_sshRPiSide
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();

            bool connectingSuccess = false;
            while (!connectingSuccess)
            {
                string adminLogin, password;

                Console.Write("Zadajte prosím administrátorský login: ");
                adminLogin = Console.ReadLine();

                Console.Write("Zadajte prosím heslo: ");
                password = ConsoleHelper.RunPasswordMode();

                connectingSuccess = server.Run(String.Format(@"Server=tcp:csshmain.database.windows.net,1433;Database=csshmain;User ID={0}@csshmain;Password={1};Encrypt=False;Connection Timeout=30;", adminLogin, password)); //Encrypt=True;TrustServerCertificate=False;

                if (connectingSuccess)
                    Console.WriteLine("\nServer sa pripojil k databáze úspešne.");
                else
                    Console.WriteLine("\nServer sa k databáze nepripojil, skúste to ešte raz.");
            }

            Console.ReadKey();
        }
    }
}
