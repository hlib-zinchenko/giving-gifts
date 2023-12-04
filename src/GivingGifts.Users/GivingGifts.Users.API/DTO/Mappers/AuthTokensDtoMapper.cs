namespace GivingGifts.Users.API.DTO.Mappers;

public static class AuthTokensDtoMapper
{
    public static AuthTokensDto ToApiDto(Users.UseCases.AuthTokensDto input)
    {
        return new AuthTokensDto
        {
            AuthToken = input.AuthToken,
        };
    }
}