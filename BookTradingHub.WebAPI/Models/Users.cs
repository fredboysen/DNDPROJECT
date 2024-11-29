using System.ComponentModel.DataAnnotations;

namespace BookTradingHub.WebAPI.Models
{
    public class User
    {
        [Key] public int user_id { get; set; }

        [Required] public string username { get; set; } = string.Empty;

        [Required] public string passwordhash { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
        public string role { get; set; } = "User"; // Default role
    }
}