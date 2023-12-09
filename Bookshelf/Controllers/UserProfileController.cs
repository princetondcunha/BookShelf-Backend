using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bookshelf.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public UserProfileController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            IQueryable<User> query = _context.Users;

            if (userId != 0)
            {
                query = query.Where(item => item.UserId == userId);
            }

            var data = query
                .Select(item => new
                {
                    item.UserId,
                    item.FirstName,
                    item.LastName,
                    item.Email,
                    item.CreatedAt
                })
                .ToList();

            if (!data.Any())
            {
                return NotFound(new { error = "Invalid UserId. The UserId does not exist." });
            }

            return Ok(data);
        }
    }
}
