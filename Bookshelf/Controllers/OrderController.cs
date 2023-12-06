using Bookshelf.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public OrderController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(int buyerId)
        {
            var data = _context.Transactions
                .Where(item => item.BuyerId == buyerId)
                .Select(item => new
                {
                    OrderId = item.TransactionId,
                    UserId = item.BuyerId,
                    item.TotalAmount,
                    OrderDate = item.TransactionDate
                })
                .ToList();

            return Ok(data);
        }
    }
}
