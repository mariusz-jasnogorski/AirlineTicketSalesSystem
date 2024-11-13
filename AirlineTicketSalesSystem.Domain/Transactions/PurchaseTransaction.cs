using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.ValueObjects;

namespace AirlineTicketSalesSystem.Domain.Transactions
{
    public class PurchaseTransaction
    {
        public Flight Flight { get; private set; }
        public Customer Customer { get; private set; }
        public Tenant Tenant { get; private set; }
        public Price FinalPrice { get; private set; }
        public List<string>? AppliedDiscounts { get; private set; }

        public PurchaseTransaction(Flight flight, Customer customer, Tenant tenant, Price finalPrice, List<string>? appliedDiscounts)
        {
            Flight = flight;
            Customer = customer;
            Tenant = tenant;
            FinalPrice = finalPrice;
            AppliedDiscounts = tenant.ShouldRecordDiscounts ? appliedDiscounts : null;
        }
    }
}
