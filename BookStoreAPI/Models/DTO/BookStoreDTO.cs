using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class BookStoreDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public BookCategoryDisplayDTO Category { get; set; }  // Navigation property
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
