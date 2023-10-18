namespace GivingGifts.Wishlists.Commands.Create;

public class CreateWishlistCommandResult
{
    public Guid Id { get; }
    public string Name { get; }

    public CreateWishlistCommandResult(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}