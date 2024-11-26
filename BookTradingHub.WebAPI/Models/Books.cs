using System.ComponentModel.DataAnnotations;
namespace BookTradingHub.WebAPI.Models

{
    public class Book
    {
        [Key]        public int Book_Id { get; set; } // Matches the database field "book_id"
        public string title { get; set; } = "";
        public string author { get; set; }= "";
        public string isbn { get; set; }= "";
        public string genre { get; set; } = "";
        public string publisher { get; set; } = "";
        public DateTime publish_date { get; set; }
        public string condition { get; set; } = "";
       
    }
}
