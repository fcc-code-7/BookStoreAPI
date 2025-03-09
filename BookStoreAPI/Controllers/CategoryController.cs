using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BookStoreAPI.Models.Domain;
using BookStoreAPI.Models.DTO;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository , IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? FilterOn , [FromQuery] string? FilterBy , [FromQuery] string? SortBy , [FromQuery] bool isAscending ,[FromQuery] int PageNo = 1,[FromQuery] int PageSize = 1000)
        {
           
            var categories = await categoryRepository.GetAllCategoryAsync(FilterOn,FilterBy,SortBy,isAscending,PageNo,PageSize);
            var categoriesDto = mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] AddRequestCategoryDTO addRequest)
        {
           var category = mapper.Map<Category>(addRequest);
           category = await categoryRepository.CreateCategoryAsync(category);
           var categoryDto = mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateRequestCategoryDTO updateRequest)
        {
            var category = mapper.Map<Category>(updateRequest);
            var updatedCategory = await categoryRepository.UpdateCategory(id, category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryDTO>(updatedCategory);
            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDto = mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }
    }
}
