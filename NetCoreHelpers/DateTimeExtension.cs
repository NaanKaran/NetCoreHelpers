using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NetCoreHelpers
{
   public static class DateTimeExtension
    {
        public static DateTime ToIST(this DateTime date)
        {
            var istDate = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            return istDate;
        }

        public static DateTime ToChinaTime(this DateTime obj)
        {
            return obj.AddHours(8);
        }

        /// <summary>
        /// "yyyy-MM-dd HH:mm:ss tt" -- 2019-05-21 12:53:09 PM
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime obj)
        {
            return obj.ToString("yyyy-MM-dd hh:mm:ss tt");
        }

        /// <summary>
        /// "yyyy-MM-dd HH:mm:ss tt" -- 2019-05-21 12:53:09 PM
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime? obj)
        {
            return obj?.ToString("yyyy-MM-dd hh:mm:ss tt");
        }

        /// <summary>
        /// like: JAN_2019-MAR_2019
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetQuadrantMonthInfo(this DateTime obj)
        {

            var month = obj.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return $@"JAN_{obj.Year}-MAR_{obj.Year}";
                case 4:
                case 5:
                case 6:
                    return $@"APR_{obj.Year}-JUN_{obj.Year}";
                case 7:
                case 8:
                case 9:
                    return $@"JUL_{obj.Year}-NOV_{obj.Year}";
                case 10:
                case 11:
                case 12:
                    return $@"OCT_{obj.Year}-DEC_{obj.Year}";
                default:
                    break;
            }

            return "";
        }

        /// <summary>
        /// like: 2019_Q1, 2019_Q2, 2019_Q3
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetQuadrantInfo(this DateTime obj)
        {

            var month = obj.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return $@"{obj.Year}_Q1";
                case 4:
                case 5:
                case 6:
                    return $@"{obj.Year}_Q2";
                case 7:
                case 8:
                case 9:
                    return $@"{obj.Year}_Q3";
                case 10:
                case 11:
                case 12:
                    return $@"{obj.Year}_Q4";
                default:
                    break;
            }

            return "";
        }

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static int GetQuadrantNumberInfo(this DateTime obj)
        {

            var month = obj.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                case 6:
                    return 2;
                case 7:
                case 8:
                case 9:
                    return 3;
                case 10:
                case 11:
                case 12:
                    return 4;
                default:
                    break;
            }

            return 0;
        }

        /// <summary>
        /// like: MMSLog_2019_Q1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetQuadrantMMSLogTableName(this DateTime obj)
        {

            var month = obj.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return $@"MMSLog_{obj.Year}_Q1";
                case 4:
                case 5:
                case 6:
                    return $@"MMSLog_{obj.Year}_Q2";
                case 7:
                case 8:
                case 9:
                    return $@"MMSLog_{obj.Year}_Q3";
                case 10:
                case 11:
                case 12:
                    return $@"MMSLog_{obj.Year}_Q4";
                default:
                    break;
            }

            return "";
        }


        /// <summary>
        /// like: SMSLog_2019_Q1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetQuadrantSMSLogTableName(this DateTime obj)
        {

            var month = obj.Month;

            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return $@"SMSLog_{obj.Year}_Q1";
                case 4:
                case 5:
                case 6:
                    return $@"SMSLog_{obj.Year}_Q2";
                case 7:
                case 8:
                case 9:
                    return $@"SMSLog_{obj.Year}_Q3";
                case 10:
                case 11:
                case 12:
                    return $@"SMSLog_{obj.Year}_Q4";
                default:
                    break;
            }

            return "";
        }
    }
}
