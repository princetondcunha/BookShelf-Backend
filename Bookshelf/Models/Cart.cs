using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
