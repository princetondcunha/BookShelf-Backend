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
            // Replace this with your actual authentication logic
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
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                // Validate the request
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the username or email already exists
                if (_context.Users.Any(u => u.Username == userRegisterRequest.Username))
                {
                    return BadRequest("Username already exists.");
                }

                if (_context.Users.Any(u => u.Email == userRegisterRequest.Email))
                {
                    return BadRequest("Email address already exists.");
                }

                // Create a new user
                var newUser = new User
                {
                    Name = userRegisterRequest.Name,
                    Username = userRegisterRequest.Username,
                    Password = userRegisterRequest.Password, // You should hash the password in a real-world scenario
                    Email = userRegisterRequest.Email
                    // Add other properties as needed
                };

                // Add the user to the database
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

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

                // You may want to implement token-based authentication here in a production scenario
                // For simplicity, let's just return a success message in this example

                return Ok($"Login successful for user: {user.Username}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }

    }
}
