using System.ComponentModel.DataAnnotations;

namespace BookTradingHub.WebAPI.Models
{
    public class Book
    {
        [Key] public int  book_Id { get; set; } required
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public string isbn { get; set; } = "";
        public string genre { get; set; } = "";
        public string publisher { get; set; } = "";
        public DateTime publish_Date { get; set; }
        public string condition { get; set; } = "";
        public double averageRating { get; set; } = 0.0; 

        public string ImageUrl {get; set;} = "";
    }
}
