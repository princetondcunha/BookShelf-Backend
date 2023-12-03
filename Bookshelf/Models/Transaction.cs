using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal DeliveryCharge { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PlatformFee { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
        public User Buyer { get; set; }
        public User Seller { get; set; }
    }
}
