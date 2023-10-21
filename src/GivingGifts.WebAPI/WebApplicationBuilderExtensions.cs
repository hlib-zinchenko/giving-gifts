using System.Text;
using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Users.Infrastructure;
using GivingGifts.Users.Infrastructure.Data;
using GivingGifts.Users.Infrastructure.JWT;
using GivingGifts.Users.UseCases;
using GivingGifts.WebAPI.Auth;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure;
using GivingGifts.Wishlists.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GivingGifts.WebAPI;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthorization();
        services
            .AddSharedKernelCore()
            .AddWishlistsDomain()
            .AddWishlistsInfrastructure(builder.Configuration)
            .AddWishlistsUseCases()
            .AddUsersInfrastructure(builder.Configuration)
            .AddUsersUseCases();

        builder.Services.Configure<JwtOptions>(
            builder.Configuration.GetSection(
                "Jwt"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextResolver, HttpUserContextResolver>();
        services.AddScoped<IUserContext>(serviceProvider =>
            serviceProvider.GetRequiredService<IUserContextResolver>().Resolve());
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}