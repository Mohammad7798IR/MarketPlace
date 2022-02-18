using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Infa.Domain.ViewModels.PublicAuth
{
    public class RegisterUserVM : GoogleReCaptchaVM
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [EmailAddress(ErrorMessage ="فرمت ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Password { get; set; }



        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [Compare("Password", ErrorMessage ="تکرار رمز عبور مغایرت دارد")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string LastName { get; set; }

    }


    public enum RegisterUserResult
    {
        Success,
        EmailExists,
        Fail
    }
}
