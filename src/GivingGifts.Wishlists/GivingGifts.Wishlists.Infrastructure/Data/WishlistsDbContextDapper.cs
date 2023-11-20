using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class WishlistsDbContextDapper
{
    private readonly IOptions<ConnectionStringsOptions> _connectionStrings;

    public WishlistsDbContextDapper(IOptions<ConnectionStringsOptions> connectionStrings)
    {
        _connectionStrings = connectionStrings;
    }
    
    public IDbConnection CreateConnection()
    { 
        return new NpgsqlConnection(_connectionStrings.Value.Wishlists);
    }
}