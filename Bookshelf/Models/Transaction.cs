using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal DeliveryCharge { get; set; } = 3;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal PlatformFee { get; set; } = 5;
        public Book? Book { get; set; }
        public User? Buyer { get; set; }
        public User? Seller { get; set; }
    }
}
