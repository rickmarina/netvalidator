using System.Drawing;

namespace NetValidator;

public class RuleBuilder<T, TProperty>
{
    private readonly T _instance;
    private readonly Validator<T> _validator;
    private readonly Func<T, TProperty> _property;
    private ValidationRules _validationRules;

    public RuleBuilder(Validator<T> validator, T instance, Func<T, TProperty> property)
    {
        _validator = validator;
        _instance = instance;
        _property = property;
        _validationRules = new ValidationRules();
    }

    public RuleBuilder<T, TProperty> NotNull()
    {
        _validationRules.Validations.Add(() => _property(_instance) != null);
        return this;
    }

    public RuleBuilder<T, TProperty> NotEmpty()
    {
        _validationRules.Validations.Add(() =>
        {
            var value = _property(_instance);
            return value switch
            {
                string s => !string.IsNullOrWhiteSpace(s),
                _ => value != null
            };
        });
        return this;
    }

    public RuleBuilder<T, TProperty> NotNullOrEmpty()
    {
        _ = NotNull();
        _ = NotEmpty();
        return this;
    }

    public RuleBuilder<T, TProperty> GreaterThan(TProperty value)
    {
        _validationRules.Validations.Add(() =>
        {
            var propertyValue = _property(_instance);

            if (propertyValue == null)
                return false;

            if (propertyValue is not IComparable comparablePropertyValue)
                throw new InvalidOperationException("Property type must be comparable.");

            return comparablePropertyValue.CompareTo(value) > 0;

        });
        return this;
    }

    public RuleBuilder<T, TProperty> LessThan(TProperty value)
    {
        _validationRules.Validations.Add(() =>
        {
            var propertyValue = _property(_instance);

            if (propertyValue == null)
                return false;

            if (propertyValue is not IComparable comparablePropertyValue)
                throw new InvalidOperationException("Property type must be comparable.");

            return comparablePropertyValue.CompareTo(value) < 0;

        });
        return this;
    }

    public RuleBuilder<T, TProperty> EqualTo(TProperty value)
    {
        _validationRules.Validations.Add(() =>
        {
            var propertyValue = _property(_instance);

            if (propertyValue == null && value == null)
                return true;

            if (propertyValue == null || value == null)
                return false;

            return propertyValue.Equals(value);

        });
        return this;
    }

    public RuleBuilder<T, TProperty> InRange(TProperty value1, TProperty value2)
    {
        _validationRules.Validations.Add(() =>
        {
            var propertyValue = _property(_instance);
            if (propertyValue is null || value1 is null || value2 is null)
                return false;

            if (propertyValue is not IConvertible convertibleProperty) 
                throw new InvalidOperationException("Property type must be convertible.");

            if (value1 is not IConvertible convertibleValue1 || value2 is not IConvertible convertibleValue2) 
                throw new InvalidOperationException("Value property type must be convertible.");

            return Convert.ToInt32(convertibleProperty) >= Convert.ToInt32(convertibleValue1) && Convert.ToInt32(convertibleProperty) <= Convert.ToInt32(convertibleValue2);

        });
        return this;
    }

    public RuleBuilder<T, TProperty> MatchesRegex(string pattern)
    {
        _validationRules.Validations.Add(() =>
        {
            if (_property(_instance) is not string propertyValue)
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(propertyValue, pattern);
        });
        return this;
    }

    public Validator<T> WithMessage(string code, string message)
    {
        _validationRules.Message = new ValidationMessage(code, message);
        _validator.AddValidationRules(_validationRules);
        return _validator;
    }

}
