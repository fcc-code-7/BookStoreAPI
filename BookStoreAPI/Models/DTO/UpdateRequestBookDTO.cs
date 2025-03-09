namespace BookStoreAPI.Models.DTO
{
    public class UpdateRequestBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }  // Foreign key
    }
}
