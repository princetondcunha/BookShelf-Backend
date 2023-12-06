using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BookshelfDbContext _context;

        public TransactionController(BookshelfDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult AddTransaction([FromBody] Transaction newTransaction)
        {
            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();

            var createdTransaction = _context.Transactions
                .Where(item => item.TransactionId == newTransaction.TransactionId)
                    .Select(item => new
                    {
                        item.TransactionId,
                        item.BookId,
                        item.BuyerId,
                        item.SellerId,
                        item.TransactionDate,
                        item.TotalAmount,
                        item.DeliveryCharge,
                        item.PlatformFee
                    })
                    .FirstOrDefault();

            return CreatedAtAction("Get", new { id = newTransaction.TransactionId }, createdTransaction);
        }
    }
}
