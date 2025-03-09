using AutoMapper;
using BookStoreAPI.Models.Domain;
using BookStoreAPI.Models.DTO;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BookController(IBookRepository bookRepository , IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] string? FilterOn, [FromQuery] string? FilterBy,[FromQuery] decimal? FilterByPrice, [FromQuery] string? SortBy, [FromQuery] bool isAscending, [FromQuery] int PageNo = 1, [FromQuery] int PageSize = 1000)
        {

            var books = await bookRepository.GetAllBooksAsync(FilterOn, FilterBy,FilterByPrice, SortBy, isAscending, PageNo, PageSize);
            var booksDto = mapper.Map<List<BookStoreDTO>>(books);
            return Ok(booksDto);
        }

        [HttpPost]
        public async Task<IActionResult> Addbook([FromBody] AddRequestBookDTO addRequest)
        {
            var book = mapper.Map<Book>(addRequest);
            book = await bookRepository.CreateBookAsync(book);
            var bookDto = mapper.Map<BookStoreDTO>(book);
            return Ok(bookDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetbookById([FromRoute] Guid id)
        {
            var book = await bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = mapper.Map<BookStoreDTO>(book);
            return Ok(bookDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Updatebook([FromRoute] Guid id, [FromBody] UpdateRequestBookDTO updateRequest)
        {
            var book = mapper.Map<Book>(updateRequest);
            var updatedbook = await bookRepository.UpdateBook(id, book);
            if (updatedbook == null)
            {
                return NotFound();
            }
            var bookDto = mapper.Map<BookStoreDTO>(updatedbook);
            return Ok(bookDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deletebook([FromRoute] Guid id)
        {
            var book = await bookRepository.DeleteBook(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = mapper.Map<BookStoreDTO>(book);
            return Ok(bookDto);
        }
    }
}
