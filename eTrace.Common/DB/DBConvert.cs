using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class DBConvert
    {
        private static DateTime? dateNull = null;
        private static char chSpace = ' ';



        public static string DB2String(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd();
        }
        public static string DB2StringUpper(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd().ToUpper();
        }

        public static string DB2StringLower(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).TrimEnd().ToLower();
        }

        public static string DB2StringBlank(object obj)
        {
            return obj == null || obj == DBNull.Value ? string.Empty : Convert.ToString(obj).Replace(" ", "_");
        }

        public static int DB2Int(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public static decimal DB2Decimal(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToDecimal(obj);
        }

        public static long DB2Long(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt64(obj);
        }

        public static double DB2Double(object obj)
        {
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToDouble(obj);
        }

        public static DateTime DB2Datetime(object obj)
        {
            return obj == null || obj == DBNull.Value ? DateTime.Now : Convert.ToDateTime(obj);
        }

        public static DateTime? DB2DatetimeNull(object obj)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(obj);
        }
        public static DateTime? DB2DatetimeNull(object obj, string formatorStr)
        { 
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(Convert.ToDateTime(obj).ToString(formatorStr));
        }
        public static string  DB2DatetimeString(object obj, string formatorStr)
        {
            return obj == null || obj == DBNull.Value ? string.Empty  : Convert.ToDateTime(obj).ToString(formatorStr);
        }

        public static DateTime? DB2DatetimeHHNull(object obj, string formatorStr)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(Convert.ToDateTime(obj).ToString(formatorStr));
        }

        public static DateTime? DB2DatetimeHHNull(object obj)
        {
            return obj == null || obj == DBNull.Value ? dateNull : Convert.ToDateTime(obj + ":00:00");
        }

        public static char DB2Char(object obj)
        {
            return obj == null || obj == DBNull.Value ? chSpace : Convert.ToChar(obj);
        }
        public static bool DB2Bool(object obj)
        {
            return obj == null || obj == DBNull.Value ? false : Convert.ToBoolean(obj);
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

        public static object String2DB(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return DBNull.Value;
            }

            return value;
        }

        public static object Int2DB(int? value)
        {
            if (!value.HasValue)
            {
                return DBNull.Value;
            }

            return value;
        }

        public static object Datetime2DB(DateTime? value)
        {
            if (!value.HasValue)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}
