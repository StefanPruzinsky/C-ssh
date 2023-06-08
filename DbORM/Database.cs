using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbORM
{
    public class Database
    {
        public DatabaseCoreHelper DatabaseCoreHelper { get; private set; }

        public Database(string connectionString)
        {
            DatabaseCoreHelper = new DatabaseCoreHelper(connectionString);
            DatabaseCoreHelper.Connect();
        }

        public void Insert(string type, string stuff)
        {
            DatabaseCoreHelper.RunCommand(String.Format("INSERT INTO [{0}]([{1}], [New]) VALUES('{2}', 1);", type, type.Substring(0, type.Length - 1), stuff));
        }

        public string Select(string type)
        {
            return DatabaseCoreHelper.RunCommand(String.Format("SELECT [{0}] FROM [{1}] WHERE [New] = 1", type.Substring(0, type.Length - 1), type)).ToString();
        }

        public void Update(string type)
        {
            DatabaseCoreHelper.RunCommand(String.Format("UPDATE [{0}] SET [New] = 0 WHERE [New] = 1;", type));
        }
    }
}
