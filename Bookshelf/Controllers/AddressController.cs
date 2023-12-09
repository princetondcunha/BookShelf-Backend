using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly BookshelfDbContext _context;
        public AddressController(BookshelfDbContext context)
        {
            _context = context;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] Address newAddress)
        {
            try
            {
                _context.Addresses.Add(newAddress);
                _context.SaveChanges();

                var createdAddress = _context.Addresses
                    .Where(item => item.AddressId == newAddress.AddressId)
                    .Select(item => new
                    {
                        item.AddressId,
                        item.UserId,
                        item.Street,
                        item.City,
                        item.State,
                        item.ZipCode,
                        item.Country,
                        item.Phone
                    })
                    .FirstOrDefault();

                if (createdAddress == null)
                {
                    return NotFound();
                }

                return CreatedAtAction("Get", new { id = newAddress.AddressId}, createdAddress);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
