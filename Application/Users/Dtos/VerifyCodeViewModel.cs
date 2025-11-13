using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Users.Dtos
{
    public class VerifyCodeViewModel
    {
        [Required]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "کد تایید الزامی است.")]
        [StringLength(6, MinimumLength = 4, ErrorMessage = "کد تایید باید بین ۴ تا ۶ رقم باشد.")]
        [Display(Name = "کد تایید")]
        public string Code { get; set; } = string.Empty;
    }
}
