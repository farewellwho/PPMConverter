using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPMConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                string usageStr = "";
                Console.WriteLine(usageStr);
            }
            else
            {
                string ppmfile = args[1];
                if (!PPMConverter.ConvertToBitmap(ppmfile))
                {
                    Console.WriteLine("Convertion failed. ");
                }
            }
        }
    }
}
