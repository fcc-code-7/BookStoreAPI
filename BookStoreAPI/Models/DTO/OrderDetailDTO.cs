using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class OrderDetailDTO
    {
        public Guid Id { get; set; }
       
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time of purc
        public OrderDTO orderDTO { get; set; }  // Navigation property
        public BookStoreDTO bookStoreDTO { get; set; }  // Navigation property
    }
}
