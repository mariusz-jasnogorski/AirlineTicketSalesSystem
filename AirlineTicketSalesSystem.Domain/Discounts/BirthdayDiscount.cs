using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.Interfaces;

namespace AirlineTicketSalesSystem.Domain.Discounts
{
    public class BirthdayDiscount : IDiscountCriteria
    {
        public string Description => "Birthday Discount";

        public bool IsSatisfiedBy(Flight flight, DateTime departureDate, Customer customer)
        {
            return customer.DateOfBirth.Day == departureDate.Day && customer.DateOfBirth.Month == departureDate.Month;
        }
    }
}
