namespace AirlineTicketSalesSystem.Domain.Entities
{
    public abstract class Tenant
    {
        public string Name { get; private set; }

        protected Tenant(string name)
        {
            Name = name;
        }

        public abstract bool ShouldRecordDiscounts { get; }
    }
}
