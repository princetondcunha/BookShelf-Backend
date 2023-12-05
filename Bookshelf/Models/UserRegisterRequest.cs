using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class UserRegisterRequest
    {
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
        // Add other properties as needed
    }
}
