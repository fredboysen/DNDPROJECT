using System.ComponentModel.DataAnnotations;
using BookTradingHub.Database.Data;
using BookTradingHub.WebAPI.Models;

namespace BookTradingHub.WebAPI.Services;

public class AuthService : IAuthService
{
    public AuthService(ApplicationDB context)


{
users = context.Users.ToList();
}

private readonly IList<User> users;


public Task<User> UserValidation(string username, string password)
{
    User? existingUser = users.FirstOrDefault(u =>
    
    u.username.Equals(username,StringComparison.OrdinalIgnoreCase)) ?? throw new Exception("User does not exist");

        if (!existingUser.password.Equals(password))
        {
            throw new Exception ("Password was incorrect");
        }

    return Task.FromResult(existingUser);
}
    

    public Task UserRegistration(User user)
    {

        if(string.IsNullOrEmpty(user.username))
        {
            throw new ValidationException("Username must be filled");


        }

        if(String.IsNullOrEmpty(user.password))
        {
          throw new ValidationException("Password must be filled");

        }
                users.Add(user);

            return Task.CompletedTask;

    }

}