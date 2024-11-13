using AirlineTicketSalesSystem.Domain.ValueObjects;
    
namespace AirlineTicketSalesSystem.Domain.Entities
{
    public class Flight
    {
        public FlightId Id { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public TimeSpan DepartureTime { get; private set; }
        public List<DayOfWeek> DepartureDays { get; private set; }

        public Flight(FlightId id, string from, string to, TimeSpan departureTime, List<DayOfWeek> departureDays)
        {
            Id = id;
            From = from;
            To = to;
            DepartureTime = departureTime;
            DepartureDays = departureDays;
        }
    }
}
