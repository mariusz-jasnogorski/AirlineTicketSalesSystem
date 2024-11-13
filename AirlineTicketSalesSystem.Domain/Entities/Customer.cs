namespace AirlineTicketSalesSystem.Domain.Entities
{
    public class Customer
    {
        public string Name { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public Customer(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}
