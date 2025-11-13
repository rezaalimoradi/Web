using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Users.Dtos
{
    public class RegisterWithPhoneViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفاً شماره موبایل خود را وارد کنید.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست. مثال: 09123456789")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
