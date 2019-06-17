namespace SkiRental.Domain.Entities
{
    public class SkiRentalCost
    {
        public int SkiId { get; set; }

        public string CustomerName { get; set; }

        public double RentCost { get; set; }
    }
}