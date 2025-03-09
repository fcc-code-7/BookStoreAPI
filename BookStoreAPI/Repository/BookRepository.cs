using BookStoreAPI.Data;
using BookStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext dbContext;

        public BookRepository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBook(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> GetAllBooksAsync(string? FilterOn = null, string? FilterBy = null, decimal? FilterByPrice = null, string? SortBy = null, bool isAscending = true, int PageNo = 1, int PageSize = 1000)
        {
            var book = dbContext.Books.AsQueryable();
            //Filter
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterBy) == false)
            {
                if (FilterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    book = book.Where(c => c.Author.Contains(FilterBy));
                }
                if (FilterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    book = book.Where(c => c.Category.ToString().Contains(FilterBy));
                }
                if (FilterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    book = book.Where(c => c.Title.Contains(FilterBy));
                }
                if (FilterOn.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    book = book.Where(c => c.Price == FilterByPrice);
                }
            }

            //Sort
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending)
                    {
                        book = book.OrderBy(c => c.Author);
                    }
                    else
                    {
                        book = book.OrderByDescending(c => c.Author);
                    }
                }
            }
            //Pagination
            var BooksSkip = (PageNo - 1) * PageSize;
            return await book.Skip(BooksSkip).Take(PageSize).Include(b => b.Category)
.Include(b => b.OrderDetails) // Ensure correct name
.ToListAsync();
        }

        
        public Task<Book?> GetBookByIdAsync(Guid id)
        {
            var book = dbContext.Books.Include(b => b.Category)
.Include(b => b.OrderDetails) // Ensure correct name
.FirstOrDefaultAsync(c => c.BookId == id);
            return book;
        }

        public async Task<Book> UpdateBook(Guid id, Book book)
        {
            var fetchBook = await dbContext.Books.FirstOrDefaultAsync(c => c.BookId == id);
            if (fetchBook == null)
            {
                return null;
            }
            fetchBook.Author = book.Author;
            fetchBook.Title = book.Title;
            fetchBook.Price = book.Price;
            fetchBook.StockQuantity = book.StockQuantity;
            fetchBook.ISBN = book.ISBN;
            fetchBook.CategoryId = book.CategoryId;
            await dbContext.SaveChangesAsync();
            return fetchBook;
        }
    }
}
