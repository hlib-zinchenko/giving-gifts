using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.Commands.Create;

public record CreateWishlistCommand(string? Name)
    : IRequest<Result<CreateWishlistCommandResult>>;