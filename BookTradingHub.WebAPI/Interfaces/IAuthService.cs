using System.ComponentModel.DataAnnotations;
using BookTradingHub.WebAPI.Models;

namespace BookTradingHub.WebAPI.Services;

public  interface IAuthService
{
    Task<User> UserValidation(string username, string password);

    Task UserRegistration(User user);


}