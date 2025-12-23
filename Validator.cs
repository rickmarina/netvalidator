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

    public Validator<T> RuleDomain(Func<T,bool> domainValidation,string code,string message)
    {
        var rules = new ValidationRules();
        rules.Validations.Add(() => domainValidation(_instance));
        rules.Message = new ValidationMessage(code, message);
        AddValidationRules(rules);

        return this; 
    }

    internal void AddValidationRules(ValidationRules rules)
    {
        _validationRules.Add(rules);
    }

    public (bool, ValidationMessage?) Validate()
    {
        foreach (var rule in _validationRules)
        {
            bool valid = rule.Validations.All(x => x());
            if (!valid)
                return (false, rule.Message);

        }
        return (true, null);
    }
}
