using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToUTC(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        public static DateTime ToStartOfDayUtc(this DateTime dateTime)
        {
            return dateTime.Date.ToUTC();
        }

        public static DateTime ToEndOfDayUtc(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1).ToUTC();
        }

        public static DateTime ToStartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        public static DateTime ToStartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime ToEndOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month)).AddDays(1).AddTicks(-1);
        }

        public static DateTime ToStartOfMonthUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).ToUTC();
        }

        public static DateTime ToEndOfMonthUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month)).AddDays(1).AddTicks(-1).ToUTC();
        }
    }
}
