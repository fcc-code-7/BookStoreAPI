using BookStoreAPI.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookStoreDbContext : IdentityDbContext<User>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }


    }
}
