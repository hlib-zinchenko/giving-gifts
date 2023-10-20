namespace GivingGifts.Wishlists.UseCases;

public class WishlistDto
{
    public WishlistDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}