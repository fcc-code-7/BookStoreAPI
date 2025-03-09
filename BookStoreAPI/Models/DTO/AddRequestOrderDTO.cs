namespace BookStoreAPI.Models.DTO
{
    public class AddRequestOrderDTO
    {
        public string UserId { get; set; }  // Foreign key
        public DateTime OrderDate { get; set; }
    }
}
