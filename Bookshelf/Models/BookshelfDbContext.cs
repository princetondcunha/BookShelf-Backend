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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookId);

            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);

            modelBuilder.Entity<BookCategoryMapping>()
                .HasKey(bcm => bcm.MappingId);

            modelBuilder.Entity<BookReview>()
                .HasKey(br => br.ReviewId);

            modelBuilder.Entity<BookCategory>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory { CategoryId = 1, CategoryName = "Fiction" },
                new BookCategory { CategoryId = 2, CategoryName = "Non-Fiction" },
                new BookCategory { CategoryId = 3, CategoryName = "Science Fiction" },
                new BookCategory { CategoryId = 4, CategoryName = "Mystery" },
                new BookCategory { CategoryId = 5, CategoryName = "Romance" },
                new BookCategory { CategoryId = 6, CategoryName = "Fantasy" },
                new BookCategory { CategoryId = 7, CategoryName = "Biography" },
                new BookCategory { CategoryId = 8, CategoryName = "Self-Help" },
                new BookCategory { CategoryId = 9, CategoryName = "History" },
                new BookCategory { CategoryId = 10, CategoryName = "Thriller" },
                new BookCategory { CategoryId = 11, CategoryName = "Children" },
                new BookCategory { CategoryId = 12, CategoryName = "Science" },
                new BookCategory { CategoryId = 13, CategoryName = "Art and Photography" },
                new BookCategory { CategoryId = 14, CategoryName = "Cooking" },
                new BookCategory { CategoryId = 15, CategoryName = "Poetry" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
