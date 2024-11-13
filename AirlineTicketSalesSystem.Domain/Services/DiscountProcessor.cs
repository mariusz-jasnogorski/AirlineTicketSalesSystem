using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.Interfaces;
using AirlineTicketSalesSystem.Domain.ValueObjects;

namespace AirlineTicketSalesSystem.Domain.Services
{
    public class DiscountProcessor
    {
        private const decimal DiscountAmount = 5m;
        private const decimal MinimumPrice = 20m;
        private List<IDiscountCriteria> _criteria;

        public DiscountProcessor()
        {
            _criteria = new List<IDiscountCriteria>();
        }

        public void AddCriteria(IDiscountCriteria criteria)
        {
            _criteria.Add(criteria);
        }

        public (Price finalPrice, List<string>? appliedDiscounts) ApplyDiscounts(Price basePrice, Flight flight, DateTime departureDate, Customer customer, Tenant tenant)
        {
            var finalAmount = basePrice.Amount;
            var appliedDiscounts = tenant.ShouldRecordDiscounts ? new List<string>() : null;

            foreach (var criteria in _criteria)
            {
                if (criteria.IsSatisfiedBy(flight, departureDate, customer) && finalAmount - DiscountAmount >= MinimumPrice)
                {
                    finalAmount -= DiscountAmount;
                    if (tenant.ShouldRecordDiscounts)
                    {
                        appliedDiscounts?.Add(criteria.Description);
                    }
                }
            }

            return (new Price(finalAmount), appliedDiscounts);
        }
    }
}
