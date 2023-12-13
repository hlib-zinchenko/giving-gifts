using System.Reflection;
using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Conventions;
using GivingGifts.SharedKernel.API;
using GivingGifts.SharedKernel.API.Middleware;
using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Core.Extensions;
using GivingGifts.Users.Infrastructure;
using GivingGifts.Users.UseCases;
using GivingGifts.WebAPI.Auth;
using GivingGifts.WebAPI.Swagger;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure;
using GivingGifts.Wishlists.UseCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace GivingGifts.WebAPI;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog();

        var services = builder.Services;
        services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddNewtonsoftJson(setupAction =>
        {
            setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        })
        .AddXmlSerializerFormatters();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddAuthorization();
        services
            .AddSharedKernelCore()
            .SharedKernelApi()
            .AddWishlistsDomain()
            .AddWishlistsInfrastructure(builder.Configuration)
            .AddWishlistsUseCases()
            .AddUsersInfrastructure(builder.Configuration)
            .AddUsersUseCases();

        services.AddMediatR(
            Assembly.GetAssembly(typeof(Users.UseCases.DependencyInjection))!,
            Assembly.GetAssembly(typeof(Wishlists.Core.DependencyInjection))!,
            Assembly.GetAssembly(typeof(Wishlists.UseCases.DependencyInjection))!);

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
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]
                                               ?? throw new NullReferenceException(
                                                   "Missing JWT:Secret section in configuration")))
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextResolver, HttpUserContextResolver>();
        services.AddScoped<IUserContext>(serviceProvider =>
            serviceProvider.GetRequiredService<IUserContextResolver>().Resolve());

        services.AddApiVersioning(
                options =>
                {
                    options.DefaultApiVersion = new ApiVersion(2.0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    // reporting api versions will return the headers
                    // "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("X-Version"),
                        new MediaTypeApiVersionReader("x-version"));
                })
            .AddMvc(
                options =>
                {
                    // automatically applies an api version based on the name of
                    // the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        services.ConfigureOptions<NamedSwaggerGenOptions>();

        services.AddScoped<ValidateDataShapingFilterAttribute>();
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}