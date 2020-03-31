using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EnCrack
{
    //Change the program assembly name on ur encryption software's whitelist
    //For example, "Acrobat.exe"
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Crack();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void Crack()
        {
            string inFileName;
            inFileName = Console.ReadLine().Trim('\"');
            byte[] fb = File.ReadAllBytes(inFileName);
            string outFileName = Path.GetDirectoryName(inFileName) + 
                '\\'+Path.GetFileNameWithoutExtension(inFileName);
            File.WriteAllBytes(outFileName, fb);

            ProcessStartInfo ps = new ProcessStartInfo("CrackRenamer.exe");
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            ps.Arguments = '\"' + outFileName + '\"' + " " 
                + '\"' + outFileName + "-Copy" + Path.GetExtension(inFileName) + '\"';

            Process.Start(ps).WaitForExit();
        }
    }
}
