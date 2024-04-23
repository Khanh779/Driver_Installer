using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_Installer.Install
{
    public class PNPUtil_Driver
    {

        string pnpUtilPath = "pnputil.exe";
        // install driver
        // command 1: pnputil -i -a <driverPath>
        // command 2: pnputil /add-driver <driverName>

        public static void InstallDriver(string driverPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = "PNPUtil.exe";
            process.StartInfo.Arguments = $"/add-driver {driverPath} /install";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(output);
        }

       
    }
}
