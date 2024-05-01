namespace Restaurant.Infrastructure.Options;

public class JwtSettings
{
    public const string SectionName = nameof(JwtSettings);

    public string Secret { get; set; } = null!;

    public int ExpireMinutes { get; set; }

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;
}