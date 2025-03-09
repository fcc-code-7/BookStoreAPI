using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class BookStoreDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public CategoryDTO categoryDTO { get; set; }  // Navigation property
        public List<OrderDetailDTO> DetailDTOs { get; set; }
    }
}
