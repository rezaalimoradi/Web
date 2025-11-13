namespace Application.Configuration;


public class AppConfig
{
	public ConnectionStrings ConnectionStrings { get; set; }
	public AppSettings AppSettings { get; set; }
	public SmtpSettings SmtpSettings { get; set; }
	public JwtSettings JwtSettings { get; set; }
	public CorsSettings CorsSettings { get; set; }
}