using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است.")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور الزامی است.")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد.")]
        public string Password { get; set; } = string.Empty;
    }
}
