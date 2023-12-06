using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public CartController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCartItem([FromBody] Cart newCartItem)
        {
            _context.Carts.Add(newCartItem);
            _context.SaveChanges();

            var addedCartItem = _context.Carts
                .Where(item => item.CartId == newCartItem.CartId)
                    .Select(item => new
                    {
                        item.CartId,
                        item.UserId,
                        item.BookId,
                        item.AddedAt
                    })
                    .FirstOrDefault();

            return CreatedAtAction("Get", new { id = newCartItem.CartId }, addedCartItem);
        }

        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var data = _context.Carts
                .Where(item => item.UserId == userId)
                .Select(item => new
                {
                    item.CartId,
                    item.UserId,
                    item.BookId,
                    item.AddedAt
                })
                .ToList();

            return Ok(data);
        }
    }
}
