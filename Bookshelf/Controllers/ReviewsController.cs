using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
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

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] BookReview review)
        {
            // Validate the input model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming ReviewModel has properties like ReviewerId, BookId, Rating, ReviewText, Timestamp
            var newReview = new BookReview
            {
                ReviewerId = review.ReviewerId,
                BookId = review.BookId,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                Timestamp = DateTime.Now // You can adjust the timestamp based on your needs
            };

            // Add the new review to the context and save changes
            _context.BookReviews.Add(review);
            _context.SaveChanges();

            var createdReview = _context.BookReviews
                .Where(item => item.ReviewId == review.ReviewId)
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

            return CreatedAtAction("Get", new { id = review.BookId }, createdReview);
        }


    }
}
