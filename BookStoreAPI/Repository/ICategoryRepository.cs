using BookStoreAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoryAsync( string? FilterOn = null, string? FilterBy = null , string? SortBy = null , bool isAscending = true , int PageNo = 1 , int PageSize = 1000);

        Task<Category> CreateCategoryAsync(Category category );

        Task<Category?> GetCategoryByIdAsync(Guid id);

        Task<Category> UpdateCategory(Guid id, Category category);

        Task<Category> DeleteCategory(Guid id);
    }
}
