using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EnCrack
{
    //Change the program assembly name on ur encryption software's whitelist
    //For example, "Acrobat.exe"
    class Program
    {
        static void Main(string[] args)
        {
            string[] ss = new string[1];
            while (true)
            {
                bool bf = false;
                try
                {
                    ss = SplitFileNames();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }                    

                foreach (var s in ss)
                {
                    try
                    {
                        Crack(s);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(s + ": " + e.Message);
                    }
                }
            }
        }

        static void Crack(string inFileName)
        {
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
            Console.WriteLine(inFileName + "   OK");
        }

        static string[] SplitFileNames()
        {
            string inFileNames;
            inFileNames = Console.ReadLine().Replace("\"","");
            var matches = Regex.Matches(inFileNames, @"[A-Z]:\\[^:]+(?=([A-Z]:)|$)");
            List<string> ss = new List<string>();
            foreach (Match m in matches)
            {
                if (!string.IsNullOrWhiteSpace(m.Value))
                    ss.Add(m.Value.Trim());
            }
            return ss.ToArray();
        }
    }
}
