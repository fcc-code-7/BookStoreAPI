namespace BookStoreAPI.Models.Domain
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }  // Foreign key
        public Guid BookId { get; set; }  // Foreign key
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time of purc
        public Order Order { get; set; }  // Navigation property
        public Book Book { get; set; }  // Navigation property
    }
}
