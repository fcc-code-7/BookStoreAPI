using BookStoreAPI.Data;
using BookStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookStoreDbContext dbContext;

        public CategoryRepository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteCategory(Guid id)
        {
            var category =  await  dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategoryAsync(string? FilterOn = null, string? FilterBy = null , string? SortBy = null, bool isAscending = true , int PageNo = 1, int PageSize = 1000)
        {
            var categories = dbContext.Categories.AsQueryable();
            //Filter
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterBy) == false)
            {
                if(FilterOn.Equals("Name" , StringComparison.OrdinalIgnoreCase))
                {
                    categories = categories.Where(c=>c.Name.Contains(FilterBy));
                }
            }

            //Sort
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending)
                    {
                        categories = categories.OrderBy(c => c.Name);
                    }
                    else
                    {
                        categories = categories.OrderByDescending(c => c.Name);
                    }
                }
            }
            //Pagination
            var categoriesSkip = (PageNo - 1) * PageSize;
            return await categories.Skip(categoriesSkip).Take(PageSize).Include(c => c.Books).ToListAsync();
        }

        public Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            var category = dbContext.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.CategoryId == id);
            return category;
        }

        public async Task<Category> UpdateCategory(Guid id, Category category)
        {
            var fetchcategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (fetchcategory == null)
            {
                return null;
            }
            fetchcategory.Name = category.Name;
            await dbContext.SaveChangesAsync();
            return fetchcategory;
        }
    }
}
