using GivingGifts.WebAPI.Auth;
using GivingGifts.Wishlists.API;
using SharedKernel;

namespace GivingGifts.WebAPI;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddSharedKernel()
            .AddWishlists();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextResolver, HttpUserContextResolver>();
        services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<IUserContextResolver>().Resolve());
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

        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
}