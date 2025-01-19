using System;

namespace CarInsuranceMVC.Models
{
    public class Insuree
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public int CarYear { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public int SpeedingTickets { get; set; }
        public bool HasDUI { get; set; }
        public bool FullCoverage { get; set; }
        public decimal Quote { get; set; }  // The calculated quote

        // Business logic to calculate the quote
        public void CalculateQuote()
        {
            decimal baseQuote = 50;

            // Age-based quote
            if (Age <= 18)
                baseQuote += 100;
            else if (Age >= 19 && Age <= 25)
                baseQuote += 50;
            else
                baseQuote += 25;

            // Car year-based quote
            if (CarYear < 2000)
                baseQuote += 25;
            else if (CarYear > 2015)
                baseQuote += 25;

            // Car make-based quote
            if (CarMake.ToLower() == "porsche")
                baseQuote += 25;

            // Speeding tickets-based quote
            baseQuote += 10 * SpeedingTickets;

            // DUI-based quote
            if (HasDUI)
                baseQuote *= 1.25m;

            // Full coverage-based quote
            if (FullCoverage)
                baseQuote *= 1.50m;

            // Set the final calculated quote
            Quote = baseQuote;
        }
    }
}
