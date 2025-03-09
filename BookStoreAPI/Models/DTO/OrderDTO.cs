using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // Enum for order status
        public UserDTO User { get; set; }  // Navigation property
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
