namespace NetValidator;

/// <summary>
/// Holds validations and message by each rule
/// </summary>
public class ValidationRules
{
    public List<Func<bool>> Validations {get; set;} = [];
    public ValidationMessage? Message {get; set;}

    public ValidationRules() {}
    public ValidationRules(List<Func<bool>> validations, ValidationMessage validationMessage)
    {
        this.Validations = validations;
        this.Message = validationMessage;
    }


}