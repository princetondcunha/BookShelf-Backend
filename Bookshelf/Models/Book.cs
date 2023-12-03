namespace Bookshelf.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
