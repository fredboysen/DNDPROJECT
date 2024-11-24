namespace BookTradingHub.WebAPI.Models
{
    public class Book
    {
        public int BookId { get; set; } // Matches the database field "book_id"
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; } // Add this if missing
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
        public string Condition { get; set; } // Add this if missing
        public bool IsAvailable { get; set; } // Example: indicates if a book is available for exchange
    }
}
