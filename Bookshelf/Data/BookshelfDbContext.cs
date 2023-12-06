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
        public DbSet<BookReview> BookReviews { get; set; }

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

            modelBuilder.Entity<BookReview>()
                .HasKey(br => br.ReviewId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

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
