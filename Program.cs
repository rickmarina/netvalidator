using System.ComponentModel.DataAnnotations;
using NetValidator;

Console.WriteLine("net Validator!");

User user = new()
{
    Name = "Alice",
    Email = "alice@test.com",
    Age = 30,
    UserCode = null
};

var isValid = Validator<User>.For(user)
                .Rule(x=> x.UserCode).NotNull()
                .Rule(x=> x.UserCode).NotEmpty()
                .Rule(x=> x.Age).GreaterThan(18)
                .Validate();

System.Console.WriteLine($"Is user valid? {isValid}");
