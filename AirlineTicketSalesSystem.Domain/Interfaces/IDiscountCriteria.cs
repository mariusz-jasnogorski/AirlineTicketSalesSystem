using AirlineTicketSalesSystem.Domain.Entities;

namespace AirlineTicketSalesSystem.Domain.Interfaces
{
    public interface IDiscountCriteria
    {
        bool IsSatisfiedBy(Flight flight, DateTime departureDate, Customer customer);

        string Description { get; }
    }
}
