using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTradingHub.WebAPI.Models
{
    public class User
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }

        [Required] 
        public string username { get; set; } = string.Empty;

        [Required] 
        public string password { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
        public string role { get; set; } = "User";
    }
}
