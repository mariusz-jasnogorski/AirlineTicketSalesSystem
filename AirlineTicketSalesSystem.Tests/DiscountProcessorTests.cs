using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.Discounts;
using AirlineTicketSalesSystem.Domain.Services;
using AirlineTicketSalesSystem.Domain.ValueObjects;
using AirlineTicketSalesSystem.Domain.Extensions;

namespace AirlineTicketSalesSystem.Tests.Services
{
    public class DiscountProcessorTests
    {
        [Fact]
        public void ApplyDiscounts_ShouldApplyAllEligibleDiscounts_AndNotGoBelowMinimumPrice()
        {
            // Arrange
            var discountProcessor = new DiscountProcessor();
            discountProcessor.AddCriteria(new BirthdayDiscount());
            discountProcessor.AddCriteria(new AfricaThursdayDiscount());

            var nextThursday = DateTime.Now.GetNextWeekday(DayOfWeek.Thursday);

            var tenantA = new TenantGroupA("Tenant A");
            var flightId = new FlightId("KLM", "12345", "BCA");
            var flight = new Flight(flightId, "Amsterdam", "Kapsztad, Africa", new TimeSpan(14, 0, 0), new List<DayOfWeek> { DayOfWeek.Thursday });
            var customer = new Customer("John Doe", new DateTime(1990, nextThursday.Month, nextThursday.Day));
            var basePrice = new Price(30m);
            var departureDate = nextThursday;

            // Act
            var (finalPrice, appliedDiscounts) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantA);

            // Assert
            Assert.Equal(20m, finalPrice.Amount);
            Assert.NotNull(appliedDiscounts);
            Assert.Contains("Birthday Discount", appliedDiscounts);
            Assert.Contains("Africa Thursday Discount", appliedDiscounts);
        }

        [Fact]
        public void ApplyDiscounts_ShouldNotApplyDiscounts_IfPriceWouldGoBelowMinimum()
        {
            // Arrange
            var discountProcessor = new DiscountProcessor();
            discountProcessor.AddCriteria(new BirthdayDiscount());

            var tenantA = new TenantGroupA("Tenant A");
            var flightId = new FlightId("KLM", "54321", "DEF");
            var flight = new Flight(flightId, "Amsterdam", "Paris", new TimeSpan(10, 0, 0), new List<DayOfWeek> { DayOfWeek.Monday });
            var customer = new Customer("Jane Doe", new DateTime(1990, DateTime.Now.Month, DateTime.Now.Day));
            var basePrice = new Price(21m);
            var departureDate = DateTime.Now;

            // Act
            var (finalPrice, appliedDiscounts) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantA);

            // Assert
            Assert.Equal(21m, finalPrice.Amount);
            Assert.NotNull(appliedDiscounts);
            Assert.Empty(appliedDiscounts);
        }

        [Fact]
        public void ApplyDiscounts_ShouldNotRecordDiscounts_ForTenantGroupB()
        {
            // Arrange
            var discountProcessor = new DiscountProcessor();
            discountProcessor.AddCriteria(new BirthdayDiscount());

            var tenantB = new TenantGroupB("Tenant B");
            var flightId = new FlightId("KLM", "12345", "BCA");
            var flight = new Flight(flightId, "Amsterdam", "Kapsztad, Africa", new TimeSpan(14, 0, 0), new List<DayOfWeek> { DayOfWeek.Thursday });
            var customer = new Customer("John Doe", new DateTime(1990, DateTime.Now.Month, DateTime.Now.Day));
            var basePrice = new Price(30m);
            var departureDate = DateTime.Now;

            // Act
            var (finalPrice, appliedDiscounts) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantB);

            // Assert
            Assert.Equal(25m, finalPrice.Amount);
            Assert.Null(appliedDiscounts);
        }
    }
}
