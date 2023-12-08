using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Authorize]
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
                item.Description,
                item.CategoryName,
                item.Author,
                item.Condition,
                item.Price,
                item.ImageUrl,
                item.CreatedAt,
                item.IsSold
            })
            .ToList();

            return Ok(data);
        }

        [HttpGet("activeListing")]
        public IActionResult GetUnsoldBooks()
        {
            var unsoldBooks = _context.Books
                .Where(book => !book.IsSold)
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
                    item.CreatedAt,
                    item.IsSold
                })
                .ToList();

            return Ok(unsoldBooks);
        }

        [HttpGet]
        public IActionResult Get(int bookId = 0)
        {
            IQueryable<Book> query = _context.Books;

            if (bookId != 0)
            {
                query = query.Where(item => item.BookId == bookId);
            }

            var data = query
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
                    item.CreatedAt,
                    item.IsSold
                })
                .ToList();

            if (!data.Any())
            {
                return NotFound(new { error = "Invalid BookId. The specified BookId does not exist." });
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
