namespace SharedKernel;

public interface IUserContext
{
    Guid UserId { get; }
    string Role { get; }
    bool IsAdmin() => Role == RoleNames.Admin;
    bool IsUser() => Role == RoleNames.User;
}