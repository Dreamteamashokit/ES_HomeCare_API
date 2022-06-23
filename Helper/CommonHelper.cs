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

        static public TimeSpan ParseTime(this string strTime, string format)
        {
            DateTime output;
            var ok = DateTime.TryParseExact(strTime, format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out output);
            return output.Subtract(output.Date);
        }

        public static DateTime ParseDate(this string dateStr)
        {
            
            if (!string.IsNullOrEmpty(dateStr))
            {
                DateTime dte = DateTime.ParseExact(dateStr.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return dte;

            }
            else
            {

                return DateTime.Now;
            }

        }







        public static DateTime ParseDate(this string dateStr, string format)
        {

            string dateTimeStr = dateStr;
            DateTime dte = DateTime.ParseExact(dateTimeStr.Trim(), format, CultureInfo.InvariantCulture);
            return dte;
        }


        public static DateTime ParseDateTime(this string dateStr)
        {

            string dateTimeStr = dateStr;
            DateTime dte = DateTime.ParseExact(dateTimeStr.Trim(), "dd-MM-yyyy, hh:mm:ss tt", CultureInfo.InvariantCulture);
            return dte;
        }






        public static string TimeHelper(this TimeSpan timeSpan)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.MinValue;
            }
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



        public static string StringIsNull(this string str)
        {
            string result = String.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                result = str;
            }
            return result;

        }



        public static string RemoveChar(string name)
        {
            string tidyName = name;

            tidyName = tidyName.Replace("&", "And").Replace("/", "And").Replace("'", "").Replace("-", "").Replace(" ", "").Replace(".", "").Replace(">", "").Replace("<", "");

            return tidyName;
        }

    }

}
