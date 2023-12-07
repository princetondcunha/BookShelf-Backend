using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Book> SellingBooks { get; set; }
        public ICollection<Cart> CartItems { get; set; }
        public ICollection<Transaction> BuyerTransactions { get; set; }
        public ICollection<Transaction> SellerTransactions { get; set; }
        public ICollection<BookReview> Reviews { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
