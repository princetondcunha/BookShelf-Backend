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
            // Configure relationships using Fluent API if needed
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Book)
                .WithMany(b => b.Carts)
                .HasForeignKey(c => c.BookId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Book)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BookId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.BuyerTransactions)
                .HasForeignKey(t => t.BuyerId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Seller)
                .WithMany(u => u.SellerTransactions)
                .HasForeignKey(t => t.SellerId);

            modelBuilder.Entity<BookCategoryMapping>()
                .HasOne(bcm => bcm.Book)
                .WithMany(b => b.CategoryMappings)
                .HasForeignKey(bcm => bcm.BookId);

            modelBuilder.Entity<BookCategoryMapping>()
                .HasOne(bcm => bcm.Category)
                .WithMany(c => c.CategoryMappings)
                .HasForeignKey(bcm => bcm.CategoryId);

            modelBuilder.Entity<BookReview>()
                .HasOne(br => br.Reviewer)
                .WithMany(u => u.BookReviews)
                .HasForeignKey(br => br.ReviewerId);

            modelBuilder.Entity<BookReview>()
                .HasOne(br => br.Book)
                .WithMany(b => b.BookReviews)
                .HasForeignKey(br => br.BookId);

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
