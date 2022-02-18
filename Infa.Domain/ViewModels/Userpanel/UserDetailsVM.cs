using Infa.Domain.Models.Identity;
using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Userpanel
{
    public class UserDetailsVM : GoogleReCaptchaVM
    {

        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Password { get; set; }


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور مغایرت دارد")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }


        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "اواتار")]
        public string ImageName { get; set; }

    }
    public enum UserDetailsResult
    {
        Success , 
        NotFound , 
        NotImage
    }
}
