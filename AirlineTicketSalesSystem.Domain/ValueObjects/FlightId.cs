using System.Text.RegularExpressions;

namespace AirlineTicketSalesSystem.Domain.ValueObjects
{
    public class FlightId
    {
        public string AirlineCode { get; private set; } // Three-letter IATA code
        public string Number { get; private set; }      // Five-digit number
        public string Suffix { get; private set; }      // Three-letter suffix

        public FlightId(string airlineCode, string number, string suffix)
        {
            if (!IsValidFlightId(airlineCode, number, suffix))
                throw new ArgumentException("Invalid Flight ID format.");

            AirlineCode = airlineCode;
            Number = number;
            Suffix = suffix;
        }

        private bool IsValidFlightId(string airlineCode, string number, string suffix)
        {
            var regex = new Regex(@"^[A-Z]{3}$");
            return regex.IsMatch(airlineCode) && regex.IsMatch(suffix) && number.Length == 5 && number.All(char.IsDigit);
        }

        public override string ToString()
        {
            return $"{AirlineCode} {Number} {Suffix}";
        }
    }
}
