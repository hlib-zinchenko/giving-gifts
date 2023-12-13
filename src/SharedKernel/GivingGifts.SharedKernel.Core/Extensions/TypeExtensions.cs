namespace GivingGifts.SharedKernel.Core.Extensions;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetGeneticTypeParametersFromImplementedInterface(
        this Type type,
        Type implementedInterfaceType)
    {
        if (!implementedInterfaceType.IsGenericType)
        {
            throw new ArgumentException();
        }

        var matchedInterface = type
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == implementedInterfaceType);

        return matchedInterface == null
            ? Array.Empty<Type>()
            : matchedInterface.GetGenericArguments();
    }
}