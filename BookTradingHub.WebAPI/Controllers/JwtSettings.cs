namespace BookTradingHub.WebAPI.Models
{
    public class JwtSettings
    {
        public string Key { get; set; } // The secret key for signing the token
        public string Issuer { get; set; } // The issuer (who created the token)
        public string Audience { get; set; } // The audience for the token (who it's intended for)
        public int ExpiryInHours { get; set; } // Token expiration time in hours
    }
}
