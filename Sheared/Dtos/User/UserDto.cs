namespace Shared.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public byte LoginFailedCount { get; set; }
        public bool Islock { get; set; }
        public string NationalCode { get; set; } = string.Empty;
        public string ActivationCode { get; set; } = string.Empty;
        public DateTime? LockTimeEnd { get; set; }
        public int? PasswordIncorrectCount { get; set; }
        public bool ForceChangePassword { get; set; }
        public bool? IsTwoFactorLogin { get; set; }
        public DateTime? LastSuccessLoginDateTime { get; set; }
        public DateTime? LastFaildLoginDateTime { get; set; }
        public int? LastFaildLoginCount { get; set; }
        public DateTime? CurrentSuccessLoginDateTime { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
