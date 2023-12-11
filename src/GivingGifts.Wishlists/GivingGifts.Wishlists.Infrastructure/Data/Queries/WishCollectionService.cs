using Dapper;
using GivingGifts.Wishlists.UseCases.GetWishCollection;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishCollectionQueryService : IWishCollectionQueryService
{
    private readonly WishlistsDbContextDapper _dbContext;

    public WishCollectionQueryService(WishlistsDbContextDapper dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserWishDto>> GetWishesAsync(Guid wishlistId, Guid[] wishIds)
    {
        const string query = """
                             SELECT WL."UserId", W."WishlistId", W."Id", W."Name", W."Url"
                             FROM "Wishes" as W
                             JOIN "Wishlists" as WL ON W."WishlistId" = WL."Id"
                             WHERE W."WishlistId" = @WishlistId AND W."Id" = ANY (@WishIds)
                             """;

        using (var connection = _dbContext.CreateConnection())
        {
            return await connection.QueryAsync<UserWishDto>(
                query,
                new { wishlistId, wishIds });
        }
    }
}