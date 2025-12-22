
using NetValidator;

Console.WriteLine("net Validator!");

User user = new()
{ 
    Name = "Alix",
    Email = "alice@test.com",
    Age = 19,
    UserCode = null
};

var isValid = Validator<User>.For(user)
                .Rule(x=> x.Age).InRange(18,99).WithMessage("Age must be between 18 and 99")
                // .Rule(x=> x.UserCode).NotNullOrEmpty().WithMessage("User code must be provided")
                // .Rule(x=> x.Age).GreaterThan(50).WithMessage("Age must be greater than 50")
                // .Rule(x=> x.Name).EqualTo("Alix").WithMessage("Name must be Alix")
                .Validate();

System.Console.WriteLine($"Is user valid? {isValid}");
