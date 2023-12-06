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

        [HttpGet("all")]
        public IActionResult GetAll()
        {

            var data = _context.Books
            .Select(item => new
            {
                item.BookId,
                item.Title,
                item.Author,
                item.CategoryName,
                item.Condition,
                item.Price,
                item.ImageUrl                
            })
            .ToList();

            return Ok(data);
        }

        [HttpGet]
        public IActionResult Get(int bookId)
        {

            var data = _context.Books
            .Where(item => item.BookId == bookId)
            .Select(item => new
            {
                item.BookId,
                item.Title,
                item.Description,
                item.CategoryName,
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

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            try
            {
                _context.Books.Add(newBook);
                _context.SaveChanges();

                var createdBook = _context.Books
                    .Where(item => item.BookId == newBook.BookId)
                    .Select(item => new
                    {
                        item.BookId,
                        item.Title,
                        item.Description,
                        item.CategoryName,
                        item.Author,
                        item.Condition,
                        item.Price,
                        item.SellerId,
                        item.ImageUrl,
                        item.CreatedAt
                    })
                    .FirstOrDefault();

                if (createdBook == null)
                {
                    return NotFound();
                }

                return CreatedAtAction("Get", new { id = newBook.BookId }, createdBook);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
