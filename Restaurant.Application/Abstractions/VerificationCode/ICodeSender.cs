namespace Restaurant.Application.Abstractions.VerificationCode;

public interface ICodeSender
{
    Task SendCode(string sendTo, string code);
}