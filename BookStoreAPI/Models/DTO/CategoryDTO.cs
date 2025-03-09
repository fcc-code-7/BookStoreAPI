using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public List<CategoryBookDisplayDTO> Books { get; set; }  // Navigation property
    }
}
