using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSalesSystem.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetNextWeekday(this DateTime startDate, DayOfWeek desiredDay)
        {
            int daysToAdd = ((int)desiredDay - (int)startDate.DayOfWeek + 7) % 7;

            if (daysToAdd == 0)
                daysToAdd = 7; 

            return startDate.AddDays(daysToAdd);
        }
    }

}
