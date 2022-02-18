using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.PublicAuth
{
    public class ResetPasswordUserVM : GoogleReCaptchaVM
    {
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور مغایرت دارد")]
        public string ConfirmPassword { get; set; }



        public string ActiveCode { get; set; }
    }

    public enum ResetPasswordResult
    {
        Success,
        Fail,
    }
}
