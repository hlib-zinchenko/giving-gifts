using Dapper;
using GivingGifts.Wishlists.UseCases.GetWish;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishQueryService : IWishQueryService
{
    private readonly WishlistsDbContextDapper _dbContext;

    public WishQueryService(WishlistsDbContextDapper dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserWishDto?> GetUserWish(Guid wishId)
    {
        const string query = """
                             SELECT WL."UserId", W."WishlistId", W."Id", W."Name", W."Url", W."Notes"
                             FROM "Wishes" as W
                             JOIN "Wishlists" as WL ON W."WishlistId" = WL."Id"
                             WHERE W."Id" = @WishId
                             """;

        using (var connection = _dbContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<UserWishDto>(query, new { wishId });
        }
    }
}