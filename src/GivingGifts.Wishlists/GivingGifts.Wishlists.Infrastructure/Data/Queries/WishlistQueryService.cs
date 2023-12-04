using GivingGifts.Wishlists.UseCases;
using GivingGifts.Wishlists.UseCases.GetWishlist;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishlistQueryService : IWishlistQueryService
{
    private readonly WishlistsDbContextDapper _dbContext;
    private readonly WishlistsDbContextEf _contextEf;

    public WishlistQueryService(WishlistsDbContextDapper dbContext, WishlistsDbContextEf contextEf)
    {
        _dbContext = dbContext;
        _contextEf = contextEf;
    }

    public async Task<WishlistWithWishesDto?> GetWishlist(Guid wishlistId)
    {
        // using (var connection = _dbContext.CreateConnection())
        // {
        //     const string query = """
        //                             SELECT
        //                                 W."UserId",
        //                                 W."Id",
        //                                 W."Name",
        //                                 ARRAY_AGG(Wish.* ORDER BY Wish."Name") as Wishes
        //                             FROM "Wishlists" W
        //                             LEFT JOIN "Wishes" Wish ON W."Id" = Wish."WishlistId"
        //                             WHERE W."Id" = @WishlistId
        //                             GROUP BY W."UserId", W."Id", W."Name"
        //                          """;
        //
        //     var result = connection.QueryFirstOrDefaultAsync<WishlistWithWishesDto>(query, new { wishlistId });
        //
        //     return result;
        // }

        var data = await _contextEf.Wishlists
            .AsNoTracking()
            .Include(c => c.Wishes)
            .FirstOrDefaultAsync(c => c.Id == wishlistId);

        return new WishlistWithWishesDto(data.UserId, data.Id, data.Name,
            data.Wishes.Select(w => new WishDto(w.Id, w.Name, w.Url)).ToArray());
    }
}