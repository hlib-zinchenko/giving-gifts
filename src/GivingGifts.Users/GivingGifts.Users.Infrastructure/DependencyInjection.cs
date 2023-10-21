using GivingGifts.Users.Core;
using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Infrastructure.Data;
using GivingGifts.Users.Infrastructure.JWT;
using GivingGifts.Users.UseCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>(options => options
            .UseNpgsql(configuration["ConnectionStrings:Users"]));

        services.Configure<JwtOptions>(
            configuration.GetSection("Jwt"));

        services
            .AddIdentityCore<User>(c =>
            {
                c.Password.RequireDigit = true;
                c.Password.RequiredLength = 8;
                c.Password.RequireLowercase = true;
                c.Password.RequireNonAlphanumeric = true;
                c.Password.RequireUppercase = true;
            })
            .AddUserManager<UserManager<User>>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<UsersDbContext>();

        services.AddScoped<IAuthTokenGenerator, AuthTokenGenerator>();

        services.AddScoped<IUserRepository, EfUserRepository>();

        return services;
    }
}