using System.ComponentModel.DataAnnotations;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class ConnectionStringsOptions
{
    [Required]
    public string Wishlists { get; set; } = null!;
}