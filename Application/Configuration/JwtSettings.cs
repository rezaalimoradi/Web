namespace Application.Configuration;

public class JwtSettings
{
	public string Secret { get; set; }
	public string Issuer { get; set; }
	public string Audience { get; set; }
	public int ExpiryMinutes { get; set; }
    public string Key { get; set; } = string.Empty;  
    public int ExpireMinutes { get; set; } = 60;
}