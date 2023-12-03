using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class BookCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string CategoryName { get; set; }
    }
}
