using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Helper
{
    public static class CommonHelper
    {

        static public TimeSpan ParseTime(this string strTime)
        {
            DateTime output;
            var ok = DateTime.TryParseExact(strTime, @"h:mm tt", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out output);
            return output.Subtract(output.Date);
        }

        public static DateTime ParseDate(this string dateStr)
        {

            string dateTimeStr = dateStr;
            DateTime dte = DateTime.ParseExact(dateTimeStr.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return dte;
        }


        public static string TimeHelper(this TimeSpan timeSpan)
        {
            DateTime time = DateTime.Today + timeSpan;
            return time.ToString("hh:mm tt");
        }
        public static int ModifyToInt(this string str)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = int.Parse(str);
            }
            return num;

        }

        public static long ModifyToLong(this string str)
        {
            long num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = long.Parse(str);
            }
            return num;

        }

        public static decimal ModifyToDecimal(this string str)
        {
            decimal num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = Convert.ToDecimal(str);
            }
            return num;

        }

    }

}
