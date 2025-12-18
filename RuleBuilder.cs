namespace NetValidator;

public class RuleBuilder<T, TProperty> 
{
    private readonly T _instance;
    private readonly Validator<T> _validator;
    private readonly Func<T, TProperty> _property;

    public RuleBuilder(Validator<T> validator, T instance, Func<T, TProperty> property)
    {
        _validator = validator;
        _instance = instance;
        _property = property;
    }

    public Validator<T> NotNull()
    {
        _validator.AddRule(() =>
            _property(_instance) != null
        );
        return _validator;
    }

    public Validator<T> NotEmpty()
    {
        _validator.AddRule(() =>
        {
            var value = _property(_instance);
            return value switch
            {
                string s => !string.IsNullOrWhiteSpace(s),
                _ => value != null
            };
        });
        return _validator;
    }

    public Validator<T> GreaterThan(TProperty value) 
    {
        _validator.AddRule(() =>
        {
            var propertyValue = _property(_instance);

            if (propertyValue == null)
                return false;

            if (propertyValue is not IComparable comparablePropertyValue)
                throw new InvalidOperationException("Property type must be comparable.");

            return comparablePropertyValue.CompareTo(value) > 0;

        });
        return _validator;
    }

}
