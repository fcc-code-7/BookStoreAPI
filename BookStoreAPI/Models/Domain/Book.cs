namespace BookStoreAPI.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }  // Foreign key
        public Category Category { get; set; }  // Navigation property
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
