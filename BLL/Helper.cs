using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace _102130052_LeDinhTu_13T1.BLL
{
    public static class Helper
    {
        public static int ComboBoxIndex(List<String> lst, string value)
        {
            int i = 0;
            for (; i < lst.Count; i++)
                if (lst[i].Equals(value))
                    return i;

            return -1;
        }

        public static double ToDouble(string s)
        {
            s = s.Replace(',', '.');
            double weeklyWage;
            double.TryParse(s, NumberStyles.Any, new NumberFormatInfo() { NumberDecimalSeparator = "." }, out weeklyWage);
            return weeklyWage;
        }
    }
}
