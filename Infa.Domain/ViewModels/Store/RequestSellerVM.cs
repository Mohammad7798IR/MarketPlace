using Infa.Domain.ViewModels.Captcha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Store
{
    public class RequestSellerVM : GoogleReCaptchaVM
    {

        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string StoreName { get; set; }


        [Display(Name = "ادرس")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Address { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string MobileNumber { get; set; }

        [Display(Name = "شماره ثابت")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} وارد کنید")]
        public string Description { get; set; }
    }


    public enum RequestSellerResult
    {
        success,
        NotFound,
        NotImage,
        HasUndergoingRequest
    }
}
