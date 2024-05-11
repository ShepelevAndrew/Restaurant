namespace Restaurant.Application.Common.Abstractions.Auth.VerificationCode;

public interface ICodeGenerator
{
    string GenerateCode();
}