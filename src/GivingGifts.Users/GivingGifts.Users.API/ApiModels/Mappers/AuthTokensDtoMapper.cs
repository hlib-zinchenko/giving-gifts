namespace GivingGifts.Users.API.ApiModels.Mappers;

public static class AuthTokensDtoMapper
{
    public static AuthTokens ToApiModel(UseCases.AuthTokensDto input)
    {
        return new AuthTokens
        {
            AuthToken = input.AuthToken,
        };
    }
}