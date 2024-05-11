namespace Restaurant.Application.Common.Abstractions.Auth.VerificationCode;

public interface ICodeSender
{
    Task SendCode(string sendTo, string code);
}