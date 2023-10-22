namespace GivingGifts.SharedKernel.Core.Constants;

public static class RoleNames
{
    public const string Admin = "Admin";
    public const string User = "User";

    public static bool IsRolesExist(string role)
    {
        return role is Admin or User;
    }
}