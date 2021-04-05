namespace AuctionManagment
{
    public class Bid
    {
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public int SalleryId { get; set; }

        public Bid(int customerId, decimal price)
        {
            CustomerId = customerId;
            Price = price;
        }
    }
}