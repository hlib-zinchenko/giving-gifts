// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GivingGifts.Users.API.ApiModels;

public class LoginRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}