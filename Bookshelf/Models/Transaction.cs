namespace Bookshelf.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal PlatformFee { get; set; }
    }
}
