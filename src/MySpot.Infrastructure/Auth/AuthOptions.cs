using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MySpot.Infrastructure.Auth;

public class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan? Expiry { get; set; }
}