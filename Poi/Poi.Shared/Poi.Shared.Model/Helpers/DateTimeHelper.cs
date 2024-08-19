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
    }
}
