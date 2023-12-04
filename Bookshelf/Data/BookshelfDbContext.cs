using Bookshelf.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Data
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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<BookCategory>()
                .HasIndex(b => b.CategoryName)
                .IsUnique();

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

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Seller)
                .WithMany(u => u.SellingBooks)
                .HasForeignKey(b => b.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Book)
                .WithMany(b => b.CartItems)
                .HasForeignKey(c => c.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Order)
                .WithMany(o => o.Transactions)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Book)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.BuyerTransactions)
                .HasForeignKey(t => t.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Seller)
                .WithMany(u => u.SellerTransactions)
                .HasForeignKey(t => t.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookCategoryMapping>()
                .HasOne(bcm => bcm.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(bcm => bcm.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookCategoryMapping>()
                .HasOne(bcm => bcm.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(bcm => bcm.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookReview>()
                .HasOne(br => br.Reviewer)
                .WithMany(u => u.Reviews)
                .HasForeignKey(br => br.ReviewerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookReview>()
                .HasOne(br => br.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
