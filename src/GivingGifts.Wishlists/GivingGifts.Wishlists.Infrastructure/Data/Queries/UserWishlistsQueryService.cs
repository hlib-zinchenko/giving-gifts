using Dapper;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.UseCases.GetUserWishlists;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class UserWishlistsQueryService : IUserWishlistsQueryService
{
    private readonly WishlistsDbContextDapper _wishlistsDbContextDapper;

    public UserWishlistsQueryService(
        WishlistsDbContextDapper wishlistsDbContextDapper)
    {
        _wishlistsDbContextDapper = wishlistsDbContextDapper;
    }
    public async Task<IEnumerable<WishlistDto>> UserWishlistsAsync(Guid userId)
    {
        const string query = 
            """SELECT W."Id", W."Name" FROM "Wishlists" as W WHERE W."UserId" = @UserId""";
        using (var connection = _wishlistsDbContextDapper.CreateConnection())
        {
            return await connection.QueryAsync<WishlistDto>(query, new { userId });
        }
    }
}