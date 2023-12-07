using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly BookshelfDbContext _context;
        public RegistrationController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Register([FromBody] User newUser)
        {
            try
            {
                // Validate the request
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the username or email already exists
                if (_context.Users.Any(u => u.Username == newUser.Username))
                {
                    var errorResponse = new
                    {
                        error = "Username already exists.",
                        status = 400
                    };

                    return BadRequest(errorResponse);
                }

                if (_context.Users.Any(u => u.Email == newUser.Email))
                {
                    var errorResponse = new
                    {
                        error = "Email address already exists.",
                        status = 400
                    };

                    return BadRequest(errorResponse);
                }

                _context.Users.Add(newUser);
                _context.SaveChanges();

                var createdUser = _context.Users
                    .Where(item => item.UserId == newUser.UserId)
                    .Select(item => new
                    {
                        item.UserId,
                        item.FirstName,
                        item.LastName,
                        item.Username,
                        item.Email,
                        item.CreatedAt
                    })
                    .FirstOrDefault();

                if (createdUser == null)
                {
                    return NotFound();
                }

                return CreatedAtAction("Get", new { id = newUser.UserId }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
