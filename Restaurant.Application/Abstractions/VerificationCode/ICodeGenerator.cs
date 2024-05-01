namespace Restaurant.Application.Abstractions.VerificationCode;

public interface ICodeGenerator
{
    string GenerateCode();
}