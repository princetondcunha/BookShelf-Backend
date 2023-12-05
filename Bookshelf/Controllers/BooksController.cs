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
                item.ImageUrl
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
                item.CreatedAt
            })
            .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // NOT FINISHED - TO BE ANALYZED ON THE DB SIDE
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
