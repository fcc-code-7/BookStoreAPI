using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; }  // Foreign key
        public string FullName { get; set; }
        public List<OrderDTO> orderDTOs { get; set; }  // Navigation property
    }
}
