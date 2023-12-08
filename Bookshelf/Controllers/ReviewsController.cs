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
    public class ReviewsController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public ReviewsController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int bookId)
        {
            var data = _context.BookReviews
                .Where(item => item.BookId == bookId)
                .Select(item => new
                {
                    item.ReviewId,
                    item.ReviewerId,
                    item.BookId,
                    item.Rating,
                    item.ReviewText,
                    item.Timestamp
                })
                .ToList();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] BookReview newReview)
        {
            _context.BookReviews.Add(newReview);
            _context.SaveChanges();

            var createdReview = _context.BookReviews
                .Where(item => item.ReviewId == newReview.ReviewId)
                    .Select(item => new
                    {
                        item.ReviewId,
                        item.ReviewerId,
                        item.BookId,
                        item.Rating,
                        item.ReviewText,
                        item.Timestamp
                    })
                    .FirstOrDefault();

            return CreatedAtAction("Get", new { id = newReview.BookId }, createdReview);
        }
    }
}
