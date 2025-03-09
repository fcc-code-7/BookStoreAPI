using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Models.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<BookStoreDTO?> BookStoreDTOs { get; set; }
    }
}
