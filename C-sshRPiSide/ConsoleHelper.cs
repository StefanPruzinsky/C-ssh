using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sshRPiSide
{
    static class ConsoleHelper
    {
        public static string RunPasswordMode()
        {
            string password = "";
            ConsoleKeyInfo consoleKeyInfo;

            while ((consoleKeyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                password += consoleKeyInfo.KeyChar;
                Console.Write("*");
            }

            return password;
        }
    }
}
