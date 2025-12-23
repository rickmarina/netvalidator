namespace NetValidator;

public class Validator<T>
{
    private readonly T _instance;
    private readonly List<ValidationRules> _validationRules = [];

    private Validator(T instance)
    {
        _instance = instance;
    }

    public static Validator<T> For(T instance)
    {
        return new Validator<T>(instance);
    }

    public RuleBuilder<T, TProperty> Rule<TProperty>(
        Func<T, TProperty> property)
    {
        return new RuleBuilder<T, TProperty>(this, _instance, property);
    }

    internal void AddValidationRules(ValidationRules rules)
    {
        _validationRules.Add(rules);
    }

    public bool Validate()
    {
        foreach (var rule in _validationRules)
        {
            bool valid = rule.Validations.All(x => x());
            if (!valid)
            {
                Console.WriteLine(rule.Message);
                return false;
            }

        }
        return true;
    }
}
