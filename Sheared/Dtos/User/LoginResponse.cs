namespace Shared.Dtos.User
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = new();
        public int ExpiresIn { get; set; }
    }
}