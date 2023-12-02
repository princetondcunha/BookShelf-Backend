using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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
    }
}
