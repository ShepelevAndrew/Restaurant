using Restaurant.Application.Abstractions.VerificationCode;

namespace Restaurant.Infrastructure.VerificationCodeServices;

public class CodeGenerator : ICodeGenerator
{
    public string GenerateCode()
    {
        var randomNumber = new Random().Next(0, 100000000);

        var formattedRandomNumber = randomNumber.ToString("D8");
        return formattedRandomNumber;
    }
}