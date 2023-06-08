using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DbORM;

namespace C_sshRemote
{
    class ConnectionManager
    {
        public Database Database { get; private set; }

        public string CurrentCommand { get; private set; }
        public List<string> TerminalPath { get; private set; }

        public ConnectionManager(string connectionString)
        {
            /*Task connectToDatabase = Task.Run(() => {

            });
            connectToDatabase.Wait();*/

            Database = new Database(connectionString);

            TerminalPath = new List<string>();
        }

        public void SendRequest(string currentCommand)
        {
            CurrentCommand = currentCommand;

            Database.Insert("Commands", String.Format("cd {0}; {1}",  String.Join("/", TerminalPath.ToArray()), currentCommand));
            //if (ConnectionString.Substring(0, 3) == "cd ")
        }

        public string GetResponse()
        {
            string response;

            try
            {
                response = Database.Select("Results");
            }
            catch
            {
                response = null;
            }

            if (response != null)
            {
                if (CurrentCommand == "cd .." && TerminalPath.Count > 0)
                    TerminalPath.RemoveAt(TerminalPath.Count - 1);
                else if (CurrentCommand.Length > 2 && CurrentCommand.Substring(0, 3) == "cd " && response == "")
                    TerminalPath.Add(String.Format("{0}/", CurrentCommand.Substring(3)));

                Database.Update("Results");

                return response;
            }
            else
                return null;
        }
    }
}
