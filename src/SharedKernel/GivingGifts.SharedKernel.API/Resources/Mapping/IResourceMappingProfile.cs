namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public interface IResourceMappingProfile<TSource, TDestination>
{
    ResourceMappingConfiguration<TSource, TDestination> CreateMappingConfiguration();
}