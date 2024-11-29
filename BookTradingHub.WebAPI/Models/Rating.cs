using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTradingHub.WebAPI.Models
{
   public class Rating
{
    [Key] public int rating_id { get; set; } // Primary Key

    [ForeignKey("user")]
    public int user_Id { get; set; }
    public User user { get; set; } = null!;

    [ForeignKey("Book")]
    public int book_id { get; set; }
    public Book book { get; set; } = null!;

    public int stars { get; set; }
    public string review { get; set; } = string.Empty;
}

}
