using AirlineTicketSalesSystem.Domain.Entities;
using AirlineTicketSalesSystem.Domain.ValueObjects;
using AirlineTicketSalesSystem.Domain.Services;
using AirlineTicketSalesSystem.Domain.Discounts;
using AirlineTicketSalesSystem.Domain.Transactions;
using AirlineTicketSalesSystem.Domain.Extensions;


var tenantA = new TenantGroupA("Tenant A");
var tenantB = new TenantGroupB("Tenant B");

// Create a flight
var flightId = new FlightId("KLM", "12345", "BCA");
var flight = new Flight(flightId, "Amsterdam", "Kapsztad, Africa", new TimeSpan(14, 0, 0), new List<DayOfWeek> { DayOfWeek.Thursday });

var nextThursday = DateTime.Now.GetNextWeekday(DayOfWeek.Thursday);

// Create a customer
var customer = new Customer("John Doe", new DateTime(1990, nextThursday.Month, nextThursday.Day));

// Set base price
var basePrice = new Price(30m);

// Set departure date
var departureDate = nextThursday;

// Setup discount processor
var discountProcessor = new DiscountProcessor();
discountProcessor.AddCriteria(new BirthdayDiscount());
discountProcessor.AddCriteria(new AfricaThursdayDiscount());

// Apply discounts for tenant A
var (finalPriceA, discountsA) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantA);

var transactionA = new PurchaseTransaction(flight, customer, tenantA, finalPriceA, discountsA);

Console.WriteLine($"Final Price for Tenant A: {finalPriceA.Amount} euros");
Console.WriteLine("Applied Discounts for Tenant A:");

if (transactionA.AppliedDiscounts != null)
{
    foreach (var discount in transactionA.AppliedDiscounts)
    {
        Console.WriteLine($"- {discount}");
    }
}


// Apply discounts for tenant B
var (finalPriceB, discountsB) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantB);

var transactionB = new PurchaseTransaction(flight, customer, tenantB, finalPriceB, discountsB);

Console.WriteLine($"\nFinal Price for Tenant B: {finalPriceB.Amount} euros");
Console.WriteLine("Applied Discounts for Tenant B:");

if (transactionB.AppliedDiscounts == null)
{
    Console.WriteLine("Discounts are not recorded for Tenant B.");
}


basePrice = new Price(21m);

// Apply discounts for tenant A
(finalPriceA, discountsA) = discountProcessor.ApplyDiscounts(basePrice, flight, departureDate, customer, tenantA);

transactionA = new PurchaseTransaction(flight, customer, tenantA, finalPriceA, discountsA);

Console.WriteLine($"\nFinal Price for Tenant A when base price is {basePrice.Amount}: {finalPriceA.Amount} euros");

if (transactionA.AppliedDiscounts?.Count > 0)
{
    Console.WriteLine("Applied Discounts for Tenant A:");

    foreach (var discount in transactionA.AppliedDiscounts)
    {
        Console.WriteLine($"- {discount}");
    }
}
else
{
    Console.WriteLine("No discounts applied for Tenant A.");
}

Console.ReadLine();

        