// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GivingGifts.Users.API.ApiModels;

public class RegisterRequest
{
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}