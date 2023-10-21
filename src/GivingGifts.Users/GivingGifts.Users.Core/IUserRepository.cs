using GivingGifts.Users.Core.Entities;

namespace GivingGifts.Users.Core;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    Task<User?> GetByEmail(string email);
}