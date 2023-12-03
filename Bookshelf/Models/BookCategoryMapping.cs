using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class BookCategoryMapping
    {
        [Key]
        public int MappingId { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
    }
}
