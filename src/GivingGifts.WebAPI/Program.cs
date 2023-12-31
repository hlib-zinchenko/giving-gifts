using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Users.Infrastructure.Data;
using GivingGifts.WebAPI;
using GivingGifts.Wishlists.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        await scope.EnsureDbCreatedAndMigrated<UsersDbContext>();
        await scope.EnsureDbCreatedAndMigrated<WishlistsDbContextEf>();
    }
}

app.Run();