namespace NetValidator;
public class Validator<T>
{
    private readonly T _instance;
    private readonly List<Func<bool>> _rules = [];

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

    internal void AddRule(Func<bool> rule)
    {
        _rules.Add(rule);
    }

    public bool Validate()
    {
        return _rules.All(rule => rule());
    }
}
