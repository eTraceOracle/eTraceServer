using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Core
{
    public class DBConvert
    {
        private static DateTime? dateNull = null;
        private static char chSpace = ' ';

        public static string ConvertDBString(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd();
        }
        public static string ConvertDBStringUpper(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd().ToUpper();
        }

        public static string ConvertDBStringLower(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd().ToLower();
        }

        public static string ConvertDBStringBlank(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).Replace(" ", "_");
        }

        public static int ConvertDBInt(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public static decimal ConvertDBDecimal(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
        }

        public static long ConvertDBLong(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt64(obj);
        }

        public static double ConvertDBDouble(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToDouble(obj);
        }

        public static DateTime ConvertDBDatetime(object obj)
        {
            return obj == null || obj == DBNull.Value ? DateTime.Now : Convert.ToDateTime(obj);
        }

        public static DateTime? ConvertDBDatetimeNull(object obj)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(obj);
        }
        public static DateTime? ConvertDBDatetimeNull(object obj, string formatorStr)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(Convert.ToDateTime(obj).ToString(formatorStr));
        }


        public static DateTime? ConvertDBDatetimeHHNull(object obj, string formatorStr)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(Convert.ToDateTime(obj).ToString(formatorStr));
        }

        public static DateTime? ConvertDBDatetimeHHNull(object obj)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(obj + ":00:00");
        }

        public static char ConvertDBChar(object obj)
        {
            return obj == null || obj == DBNull.Value ? chSpace : Convert.ToChar(obj);
        }

        /// <summary>
        /// 程序执行时间测试
        /// </summary>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        /// <returns>返回(分)单位，比如: 1分钟</returns>
        public static long ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            TimeSpan timeSpan = dateEnd - dateBegin;
            //你想转的格式
            return Convert.ToInt64(timeSpan.TotalMinutes);
        }

        public static object GetStringDbData(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DBNull.Value;
            }

            return value;
        }

        public static object GetIntDbData(int? value)
        {
            if (!value.HasValue)
            {
                return DBNull.Value;
            }

            return value;
        }

        public static object GetDatetimeDbData(DateTime? value)
        {
            if (!value.HasValue)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}
