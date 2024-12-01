using System.ComponentModel.DataAnnotations;

namespace BookTradingHub.WebAPI.Models
{
    public class Rating
    {
        [Key]
        public int rating_id { get; set; }

        public int book_Id { get; set; }  // Foreign key to the Book entity
        public Book? Book { get; set; }    // Navigation property to the Book entity

        public string? title { get; set; }  // Title should be optional
        public int stars { get; set; }
        public string review { get; set; } = string.Empty;
    }
}
