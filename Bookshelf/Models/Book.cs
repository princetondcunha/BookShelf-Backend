using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        [MaxLength(50)]
        public string Condition { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int SellerId { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public bool IsSold { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? Seller { get; set; }
        public ICollection<Cart>? CartItems { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<BookReview>? Reviews { get; set; }
    }

}