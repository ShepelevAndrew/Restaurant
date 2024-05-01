using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Restaurant.Application.Abstractions.VerificationCode;
using Restaurant.Infrastructure.Options;

namespace Restaurant.Infrastructure.VerificationCodeServices;

public class EmailCodeSender : ICodeSender
{
    private readonly EmailOptions _emailOptions;

    public EmailCodeSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task SendCode(string sendTo, string code)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailOptions.FromEmail));
        email.To.Add(MailboxAddress.Parse(sendTo));

        email.Subject = "Verification code for restaurant!";
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = VerificationCodeEmail.Html(sendTo, code)
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_emailOptions.MailServer, _emailOptions.MailPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailOptions.FromEmail, _emailOptions.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}