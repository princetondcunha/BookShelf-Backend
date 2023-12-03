using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Models
{
    public class BookshelfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BookCategoryMapping> BookCategoryMappings { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        public BookshelfDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
