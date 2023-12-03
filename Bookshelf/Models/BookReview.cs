namespace Bookshelf.Models
{
    public class BookReview
    {
        public int ReviewId { get; set; }
        public int ReviewerId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
