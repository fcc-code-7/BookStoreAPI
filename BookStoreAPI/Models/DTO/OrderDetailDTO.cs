using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class OrderDetailDTO
    {
        public Guid OrderDetailId { get; set; }
       
        public int Quantity { get; set; }
        public decimal Price { get; set; }  // Price at the time of purc
        public OrderDTO Order { get; set; }  // Navigation property
        public BookStoreDTO Book { get; set; }  // Navigation property
    }
}
