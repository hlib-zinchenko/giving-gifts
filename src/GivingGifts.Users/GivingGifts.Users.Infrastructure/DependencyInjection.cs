using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(options => options
            .UseSqlite(""));

        services
            .AddIdentityCore<User>(c =>
            {
                c.Password.RequireDigit = true;
                c.Password.RequiredLength = 8;
                c.Password.RequireLowercase = true;
                c.Password.RequireNonAlphanumeric = true;
                c.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddUserManager<User>();

        return services;
    }
}