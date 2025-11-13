using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Users.Dtos
{
    public class LoginWithPhoneViewModel
    {
        [Required(ErrorMessage = "شماره موبایل الزامی است.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
