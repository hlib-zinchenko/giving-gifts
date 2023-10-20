using GivingGifts.Users.Core;
using GivingGifts.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Users.Infrastructure.Data;

public class EfUserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public EfUserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetById(Guid id)
    {
        return _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetByEmail(string email)
    {
        return _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email != null && u.Email.ToLower() == email.ToLower());
    }
}