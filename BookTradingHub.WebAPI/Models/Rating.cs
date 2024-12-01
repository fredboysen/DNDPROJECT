using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTradingHub.WebAPI.Models
{
   public class Rating
{
    [Key]
    public int rating_id { get; set; }

    [ForeignKey("Book")]
    public int book_id { get; set; }

    public string? title { get; set; }  // Title should be optional
    public int stars { get; set; }
    public string review { get; set; } = string.Empty;
}
}
