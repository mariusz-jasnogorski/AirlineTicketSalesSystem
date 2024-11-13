using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.Interfaces;

namespace AirlineTicketSalesSystem.Domain.Discounts
{
    public class AfricaThursdayDiscount : IDiscountCriteria
    {
        public string Description => "Africa Thursday Discount";

        public bool IsSatisfiedBy(Flight flight, DateTime departureDate, Customer customer)
        {
            return flight.To.Contains("Africa", StringComparison.OrdinalIgnoreCase) && 
                flight.DepartureDays.Contains(DayOfWeek.Thursday) && departureDate.DayOfWeek == DayOfWeek.Thursday;
        }
    }
}
