using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Models
{
    public class BookshelfDbContext : DbContext
    {
        public DbSet<User> Users{ get; set; }

        public BookshelfDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
