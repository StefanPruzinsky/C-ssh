using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbORM;

namespace C_sshRPiSide
{
    static class ConnectionManager
    {
        public static Database Database { get; private set; }

        public static bool ConnectToDatabase(string connectionString)
        {
            bool connectingSuccess;

            try
            {
                Database = new Database(connectionString);

                connectingSuccess = true;
            }
            catch(Exception ex)//can be...cannot be
            {
                Console.WriteLine(ex.ToString());
                connectingSuccess = false;
            }

            return connectingSuccess;
        }

        public static string GetRequest()
        {
            string request;

            try
            {
                request = Database.Select("Commands");
            }
            catch
            {
                request = null;
            }

            return request;
        }

        public static bool SendResponse(string response)
        {
            bool sendingResponseSuccess;

            try
            {
                Database.Insert("Results", response);
                Database.Update("Commands");

                sendingResponseSuccess = true;
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                sendingResponseSuccess = false;
            }

            return sendingResponseSuccess;
        }
    }
}
