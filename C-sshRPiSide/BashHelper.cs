using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace C_sshRPiSide
{
    class BashHelper
    {
        public Process BashProcess { get; private set; }

        public BashHelper()
        {
            BashProcess = new Process();
            BashProcess.StartInfo.FileName = "/bin/bash";
            BashProcess.StartInfo.UseShellExecute = false;
            BashProcess.StartInfo.RedirectStandardOutput = true;
        }

        public string Execute(string command)
        {
            string response = "";

            try
            {
                BashProcess.StartInfo.Arguments = String.Format("-c \" {0} \"", command);
                BashProcess.Start();

                while (!BashProcess.StandardOutput.EndOfStream)
                    response += BashProcess.StandardOutput.ReadLine();
            }
            catch
            {
                response = null;
            }

            return response;
        }
    }
}
