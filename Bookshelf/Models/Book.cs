using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Condition is required")]
        [MaxLength(50)]
        public string Condition { get; set; }

        [Required(ErrorMessage = "CategoryName is required")]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "SellerId is required")]
        public int SellerId { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }
        public bool IsSold { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? Seller { get; set; }
        public ICollection<Cart>? CartItems { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<BookReview>? Reviews { get; set; }
    }

}
