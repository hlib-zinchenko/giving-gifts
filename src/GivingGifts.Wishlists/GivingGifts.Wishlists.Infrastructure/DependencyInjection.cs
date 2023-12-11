using GivingGifts.SharedKernel.Core.Extensions;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure.Data;
using GivingGifts.Wishlists.Infrastructure.Data.Queries;
using GivingGifts.Wishlists.UseCases.GetWish;
using GivingGifts.Wishlists.UseCases.GetWishCollection;
using GivingGifts.Wishlists.UseCases.GetWishes;
using GivingGifts.Wishlists.UseCases.GetWishlist;
using GivingGifts.Wishlists.UseCases.GetWishlists;
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
        services.AddDbContext<WishlistsDbContextEf>(options => options
            .UseNpgsql(configuration["ConnectionStrings:Wishlists"]));
        services.AddScoped<IWishlistRepository, EfWishlistRepository>();
        services.AddValidatableOptions<ConnectionStringsOptions>("ConnectionStrings");
        services.AddScoped<WishlistsDbContextDapper>();

        services.AddScoped<IWishlistsQueryService, WishlistsQueryService>();
        services.AddScoped<IWishCollectionQueryService, WishCollectionQueryService>();
        services.AddScoped<IWishlistQueryService, WishlistQueryService>();
        services.AddScoped<IWishQueryService, WishQueryService>();
        services.AddScoped<IWishesQueryService, WishesQueryService>();
        
        return services;
    }
}