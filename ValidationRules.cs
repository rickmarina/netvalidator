namespace NetValidator;

/// <summary>
/// Holds validations and message by each rule
/// </summary>
public class ValidationRules
{
    public List<Func<bool>> validations {get; set;} = [];
    public string message {get; set;} = string.Empty;

    public ValidationRules() {}
    public ValidationRules(List<Func<bool>> validations, string message)
    {
        this.validations = validations;
        this.message = message;
    }


}