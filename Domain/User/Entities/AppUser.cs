using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public byte LoginFailedCount { get; set; }
        public bool Islock { get; set; }
        public string NationalCode { get; set; } = string.Empty;
        public string ActivationCode { get; set; } = string.Empty;
        public DateTime? LockTimeEnd { get; set; }
        public int? PasswordIncorrectCount { get; set; }
        public bool ForceChangePassword { get; set; }
        public bool? IsTwoFactorLogin { get; set; }

    }
}
