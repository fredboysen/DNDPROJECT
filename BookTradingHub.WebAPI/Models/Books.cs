using System.ComponentModel.DataAnnotations;

namespace BookTradingHub.WebAPI.Models
{
    public class Book
    {
        [Key] public int  book_Id { get; set; } 

        [Required]
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public string genre { get; set; } = "";
        public double averageRating { get; set; } = 0.0; 

        public string ImageUrl {get; set;} = "";
    }
}
