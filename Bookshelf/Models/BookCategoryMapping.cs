using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class BookCategoryMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MappingId { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        public Book Book { get; set; }
        public BookCategory Category { get; set; }
    }
}
