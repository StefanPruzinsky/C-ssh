using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DbORM;
using System.Threading;

namespace C_sshRPiSide
{
    class Server
    {
        public BashHelper BashHelper { get; private set; }
        //public Database Database { get; private set; }

        public Timer Timer { get; private set; }

        public Server()
        {
            BashHelper = new BashHelper();
        }

        public bool Run(string connectionStringToDatabase)
        {
            bool connectingSuccess = ConnectionManager.ConnectToDatabase(connectionStringToDatabase);

            if (connectingSuccess)
                Timer = new Timer(new TimerCallback(Timer_Tick), null, 0, 1000);

            return connectingSuccess;
        }

        private void Timer_Tick(Object stateInfo)
        {
            string request = ConnectionManager.GetRequest();

            if (request != null)
            {
                Console.WriteLine("Požiadavka zachytená.");

                //Timer = null;

                string response = BashHelper.Execute(request);

                if (response == null)
                {
                    Console.WriteLine("Chyba pri spúšťaní príkazu.");
                    return;
                }
                  
                bool sendingResponseSuccess = ConnectionManager.SendResponse(response);

                if (sendingResponseSuccess == false)
                    Console.WriteLine("Chyba v odosielaní odpovede.");
                else
                    Console.WriteLine("Požiadavka splnená.");

                //Timer = new Timer(new TimerCallback(Timer_Tick), null, 0, 1000);
            }
            else
                Console.WriteLine("Požiadavka nezachytená.");
        }
    }
}
