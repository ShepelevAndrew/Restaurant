namespace Restaurant.Infrastructure.Options;

public record EmailOptions
{
    public const string SectionName = "EmailSettings";

    public string MailServer { get; set; } = null!;
    public string FromEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int MailPort { get; set; }
}