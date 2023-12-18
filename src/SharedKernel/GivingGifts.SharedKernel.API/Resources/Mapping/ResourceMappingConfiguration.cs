using System.Linq.Expressions;
using System.Reflection;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public class ResourceMappingConfiguration<TSource, TDestination>
{
    private record Mapping(PropertyInfo PropertyInfo, Expression Expression);

    private readonly List<Mapping> _mappings = [];
    private readonly Dictionary<string, List<SortablePropertyInfo>> _sortings = [];
    private readonly ParameterExpression _sourceParameter = Expression.Parameter(typeof(TSource), "source");

    public ResourceMappingConfiguration<TSource, TDestination> CreateMap<TProperty>(
        Expression<Func<TSource, TProperty>> source,
        Expression<Func<TDestination, TProperty>> destinationProperty,
        Action<PropertySortingConfiguration<TSource>> sortingConfigurationAction)
    {
        var sortingConfiguration = new PropertySortingConfiguration<TSource>();
        sortingConfigurationAction.Invoke(sortingConfiguration);

        _sortings[((MemberExpression)destinationProperty.Body).Member.Name]
            = sortingConfiguration.GetSortablePropertyInfos().ToList();
        return CreateMapInternal(source, destinationProperty);
    }

    public ResourceMappingConfiguration<TSource, TDestination> CreateMap<TProperty>(
        Expression<Func<TSource, TProperty>> source,
        Expression<Func<TDestination, TProperty>> destinationProperty,
        bool applyDefaultSorting = false)
    {
        if (applyDefaultSorting)
        {
            _sortings[((MemberExpression)destinationProperty.Body).Member.Name.ToLowerInvariant()] =
            [
                new SortablePropertyInfo(((MemberExpression)source.Body).Member.Name, false)
            ];
        }

        return CreateMapInternal(source, destinationProperty);
    }

    private ResourceMappingConfiguration<TSource, TDestination> CreateMapInternal<TProperty>(
        Expression<Func<TSource, TProperty>> source,
        Expression<Func<TDestination, TProperty>> destinationProperty)
    {
        var sourceValueParameter = source.Parameters[0];
        var sourceFixed = ParameterReplacer.ReplaceParameter(
            source,
            sourceValueParameter,
            _sourceParameter);
        var sourceInvocation = Expression.Invoke(sourceFixed, _sourceParameter);

        var destinationPropertyName = ((MemberExpression)destinationProperty.Body).Member.Name;
        var destinationProp = typeof(TDestination).GetProperty(destinationPropertyName)
                              ?? throw new Exception();
        var mapping = new Mapping(destinationProp, sourceInvocation);
        _mappings.Add(mapping);
        return this;
    }

    public DedicatedResourceMapper<TSource, TDestination> BuildMapper()
    {
        var body = Expression.New(typeof(TDestination));
        var memberBindings = _mappings
            .Select(mapping => Expression.Bind(mapping.PropertyInfo, mapping.Expression))
            .Cast<MemberBinding>();

        var memberInit = Expression.MemberInit(body, memberBindings);
        var lambda = Expression
            .Lambda<Func<TSource, TDestination>>(memberInit, false, _sourceParameter);

        return new DedicatedResourceMapper<TSource, TDestination>(
            lambda.Compile(),
            _sortings);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        private readonly Expression _replacement;

        private ParameterReplacer(ParameterExpression parameter, Expression replacement)
        {
            _parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _replacement = replacement ?? throw new ArgumentNullException(nameof(replacement));
        }

        public static Expression ReplaceParameter(Expression expression, ParameterExpression parameter,
            Expression replacement)
        {
            return new ParameterReplacer(parameter, replacement).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _parameter ? _replacement : base.VisitParameter(node);
        }
    }

    public class PropertySortingConfiguration<T>
    {
        private readonly Dictionary<string, bool> _propertyConfiguration = [];

        public PropertySortingConfiguration<T> Add<TProperty>(
            Expression<Func<T, TProperty>> source,
            bool isReverse = false)
        {
            _propertyConfiguration[((MemberExpression)source.Body).Member.Name.ToLowerInvariant()] = isReverse;
            return this;
        }

        public IEnumerable<SortablePropertyInfo> GetSortablePropertyInfos()
        {
            return _propertyConfiguration.Select(s => new SortablePropertyInfo(s.Key, s.Value));
        }
    }
}