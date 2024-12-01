using System.ComponentModel.DataAnnotations;

namespace BookTradingHub.WebAPI.Models
{
    public class Rating
    {
        [Key]
        public int rating_id { get; set; }

        public int book_Id { get; set; } 
        public Book? Book { get; set; } 

        public string? title { get; set; }
        public int stars { get; set; }
        public string review { get; set; } = string.Empty;
    }
}
