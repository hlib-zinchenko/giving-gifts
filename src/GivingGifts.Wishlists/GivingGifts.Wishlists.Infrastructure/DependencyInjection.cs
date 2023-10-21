using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlistsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<WishlistsDbContext>(options => options
            .UseNpgsql(configuration["ConnectionStrings:Wishlists"]));
        services.AddScoped<IWishlistRepository, EfWishlistRepository>();
        return services;
    }
}