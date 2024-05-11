using Restaurant.Application.Common.Abstractions.Auth.VerificationCode;

namespace Restaurant.Infrastructure.Auth.VerificationCodeServices;

public class CodeGenerator : ICodeGenerator
{
    public string GenerateCode()
    {
        var randomNumber = new Random().Next(0, 100000000);

        var formattedRandomNumber = randomNumber.ToString("D8");
        return formattedRandomNumber;
    }
}