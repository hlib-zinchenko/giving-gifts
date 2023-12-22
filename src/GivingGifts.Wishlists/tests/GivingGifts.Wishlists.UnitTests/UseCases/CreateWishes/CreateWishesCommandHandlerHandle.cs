using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using GivingGifts.Wishlists.UseCases.CreateWishes;
using NSubstitute;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.UseCases.CreateWishes;

public class CreateWishesCommandHandlerHandle
{
    private readonly CreateWishesCommandHandler _sut;
    private readonly IWishlistRepository _repository = Substitute.For<IWishlistRepository>();
    private readonly IUserContext _userContext = Substitute.For<IUserContext>();

    public CreateWishesCommandHandlerHandle()
    {
        _sut = new CreateWishesCommandHandler(_repository, _userContext);
    }

    [Fact]
    public async Task ReturnsSuccessResultWithCreatedWishes()
    {
        // arrange
        var currentUserId = Guid.NewGuid();
        var wishlistId = Guid.NewGuid();
        var wishlistsCollection = new[]
        {
            new Wishlist(Guid.NewGuid(), Guid.NewGuid(), "TestName1"),
            new Wishlist(wishlistId, currentUserId, "TestWishlist"),
        };

        _userContext.UserId.Returns(_ => currentUserId);
        _repository.GetAsync(Arg.Any<Guid>())
            .Returns(p => Task.FromResult(
                wishlistsCollection.FirstOrDefault(w => w.Id == (Guid)p[0])));
        _repository.SaveChangesAsync().Returns(_ => Task.CompletedTask);

        var createWishDto1 = new CreateWishDto("WishName1", "WishUrl1", "WishUrlNotes1");
        var createWishDto2 = new CreateWishDto("WishName2", "WishUrl2", "WishUrlNotes2");
        var command = new CreateWishesCommand(wishlistId, [createWishDto1, createWishDto2]);

        // act
        var result = await _sut.Handle(command, CancellationToken.None);

        // assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(2, result.Value.Length);
        Assert.Single(result.Value,
            r => r.Name == createWishDto1.Name
                 && r.Url == createWishDto1.Url
                 && r.Notes == createWishDto1.Notes);
        Assert.Single(result.Value,
            r => r.Name == createWishDto2.Name
                 && r.Url == createWishDto2.Url
                 && r.Notes == createWishDto2.Notes);
        Received.InOrder(() =>
        {
            _repository.GetAsync(command.WishlistId);
            _repository.SaveChangesAsync();
        });
    }

    [Fact]
    public async Task ReturnsNotFoundWhenWishlistDoesNotBelongToUser()
    {
        var currentUserId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var wishlistId = Guid.NewGuid();
        var existingWishlist = new Wishlist(wishlistId, otherUserId, "TestName");

        _userContext.UserId.Returns(_ => currentUserId);
        _repository.GetAsync(Arg.Any<Guid>())!
            .Returns(_ => Task.FromResult(existingWishlist));
        _repository.SaveChangesAsync().Returns(_ => Task.CompletedTask);
        var command = new CreateWishesCommand(wishlistId, [
            new CreateWishDto("TestName", null, null),
        ]);

        var result = await _sut.Handle(command, CancellationToken.None);

        Assert.Equal(ResultStatus.NotFound, result.Status);
    }

    [Fact]
    public async Task ReturnsNotFoundWhenWishlistDoesNotExists()
    {
        // arrange
        var currentUserId = Guid.NewGuid();
        var wishlistId = Guid.NewGuid();

        _userContext.UserId.Returns(_ => currentUserId);
        _repository.GetAsync(Arg.Any<Guid>())
            .Returns(_ => Task.FromResult<Wishlist?>(null));
        _repository.SaveChangesAsync().Returns(_ => Task.CompletedTask);
        var command = new CreateWishesCommand(wishlistId, [
            new CreateWishDto("TestName", null, null),
        ]);

        // act
        var result = await _sut.Handle(command, CancellationToken.None);

        // assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}