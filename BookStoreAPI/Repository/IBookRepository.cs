using BookStoreAPI.Models.Domain;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync(string? FilterOn = null, string? FilterBy = null, decimal? FilterByPrice = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000);

        Task<Book> CreateBookAsync(Book book);

        Task<Book?> GetBookByIdAsync(Guid id);

        Task<Book> UpdateBook(Guid id, Book book);

        Task<Book> DeleteBook(Guid id);
    }
}
