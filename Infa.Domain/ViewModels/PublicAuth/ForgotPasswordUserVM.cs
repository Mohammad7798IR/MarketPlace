using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.PublicAuth
{
    public class ForgotPasswordUserVM : GoogleReCaptchaVM
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }



    }

    public enum ForgotPasswordResult
    {
        Success,
        Fail,
        NotFound,
        InActive
    }
}
