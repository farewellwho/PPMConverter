using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PPMConverter
{
    public class Util
    {

        public static string changeSuffix(string str, string suffix)
        {
            int pointIndex = str.LastIndexOf('.');
            if (pointIndex >= 0)
            {
                return str.Substring(0, pointIndex + 1) + suffix;
            }
            else
            {
                return str;
            }
        }
    }
}
