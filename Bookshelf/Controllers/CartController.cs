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

        [HttpPost("addItem")]
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

        [HttpGet("getItem")]
        public IActionResult GetAll(int userId)
        {

            if (userId == default(int))
            {
                return BadRequest(new { error = "UserId is required in the query." });
            }

            var data = _context.Carts
                    .Where(item => item.UserId == userId)
                    .Join(
                        _context.Books,
                        cartItem => cartItem.BookId,
                        book => book.BookId,
                        (cartItem, book) => new
                        {
                            cartItem.CartId,
                            cartItem.UserId,
                            cartItem.BookId,
                            cartItem.AddedAt,
                            BookTitle = book.Title
                        })
                    .ToList();

            return Ok(data);
        }

        [HttpDelete("removeItem")]
        public IActionResult RemoveItem(int userId, int bookId)
        {
            if (userId == default(int) || bookId == default(int))
            {
                return BadRequest(new { error = "UserId and BookId are required in the query." });
            }

            // Find the item in the cart
            var cartItem = _context.Carts
                .FirstOrDefault(item => item.UserId == userId && item.BookId == bookId);

            if (cartItem == null)
            {
                return NotFound(new { error = "Item not found in the cart." });
            }

            // Remove the item from the cart
            _context.Carts.Remove(cartItem);
            _context.SaveChanges();

            return Ok(new { message = "Item removed from the cart successfully." });
        }

        [HttpDelete("removeAllItems")]
        public IActionResult RemoveAllItems(int userId)
        {
            if (userId == default(int))
            {
                return BadRequest(new { error = "UserId is required in the query." });
            }

            // Find all items in the cart for the user
            var cartItems = _context.Carts
                .Where(item => item.UserId == userId)
                .ToList();

            if (cartItems.Count == 0)
            {
                return NotFound(new { error = "No items found in the cart for the specified user." });
            }

            // Remove all items from the cart
            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();

            return Ok(new { message = "All items removed from the cart successfully." });
        }
    }
}
