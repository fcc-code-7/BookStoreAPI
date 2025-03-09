using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }  // Enum for order status
        public UserDTO UserDTO { get; set; }  // Navigation property
        public List<OrderDetailDTO> OrderDetailDTOs { get; set; }
    }
}
