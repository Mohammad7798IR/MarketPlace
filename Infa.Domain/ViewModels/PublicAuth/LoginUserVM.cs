using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.PublicAuth
{
    public class LoginUserVM : GoogleReCaptchaVM
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Email { get; set; }


        [Display(Name =" رمز عبور ")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "مرا به خاطر بسپار ")]
        public bool RememberMe { get; set; }
    }

    public enum LoginUserResult
    {
        Success , 
        InActive ,
        NotFound
    }
}
