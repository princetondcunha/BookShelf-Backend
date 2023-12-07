using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BookshelfDbContext _context;
        public LoginController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] LoginRequest model)
        {
            if (IsValidUser(model.Username, model.Password))
            {
                return Ok(new { message = "Password accepted" });
            }
            else
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // Replace these with your actual static username and password
            const string validUsername = "exampleuser";
            const string validPassword = "examplepassword";

            // Check if the provided credentials match the static ones
            return username == validUsername && password == validPassword;
        }

        //code for register and login

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // Validate the request
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the user by username
                var user = _context.Users
                    .SingleOrDefault(u => u.Username == loginRequest.Username);

                // Check if the user exists and verify the password
                if (user == null || user.Password != loginRequest.Password)
                {
                    return BadRequest("Invalid username or password.");
                }
                return Ok($"Login successful for user: {user.Username}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

    }
}
