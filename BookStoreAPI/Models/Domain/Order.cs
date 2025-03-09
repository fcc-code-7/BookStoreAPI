namespace BookStoreAPI.Models.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }  // Foreign key
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // Enum for order status
        public User User { get; set; }  // Navigation property
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
