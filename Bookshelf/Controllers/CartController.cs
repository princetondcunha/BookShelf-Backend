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
            var existingCartItem = _context.Carts
            .FirstOrDefault(item => item.UserId == newCartItem.UserId && item.BookId == newCartItem.BookId);

            if (existingCartItem != null)
            {
                // if item is already in the cart it will throw the error
                return Conflict(new { error = "The item is already in the cart." });
            }
            // Item does not exist, add it to the cart
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

        [HttpDelete("deleteItem")]
        public IActionResult DeleteCartItem(int cartItemId)
        {
            try
            {
                var cartItem = _context.Carts.Find(cartItemId);

                if (cartItem == null)
                {
                    return NotFound(new { error = $"CartItem with ID {cartItemId} not found." });
                }

                _context.Carts.Remove(cartItem);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while deleting the item from the cart." });
            }
        }
    }
}
