using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public BooksController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var data = _context.Books
            .Select(item => new
            {
                item.BookId,
                item.Title,
                item.Author,
                item.Condition,
                item.Price,
                item.ImageUrl,
                Categories = _context.BookCategoryMappings
                .Where(mapping => mapping.BookId == item.BookId)
                .Select(mapping => new
                {
                    mapping.CategoryId,
                    CategoryName = _context.BookCategories
                        .Where(category => category.CategoryId == mapping.CategoryId)
                        .Select(category => category.CategoryName)
                        .FirstOrDefault()
                })
                .ToList()
            })
            .ToList();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var data = _context.Books
            .Where(item => item.BookId == id)
            .Select(item => new
            {
                item.BookId,
                item.Title,
                item.Description,
                item.Author,
                item.Condition,
                item.Price,
                item.ImageUrl,
                item.CreatedAt,
                Categories = _context.BookCategoryMappings
                .Where(mapping => mapping.BookId == item.BookId)
                .Select(mapping => new
                {
                    mapping.CategoryId,
                    CategoryName = _context.BookCategories
                        .Where(category => category.CategoryId == mapping.CategoryId)
                        .Select(category => category.CategoryName)
                        .FirstOrDefault()
                })
                .ToList()
            })
            .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        //NOT FINISHED - RESPONSE TO BE RESTRUCTURED

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            try
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();

                return CreatedAtAction("Get", new { id = newBook.BookId }, newBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
