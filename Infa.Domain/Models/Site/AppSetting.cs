using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Models.Site
{
    public class AppSetting
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن")]
        public string Phone { get; set; }

        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Display(Name = "متن فوتر")]
        public string FooterText { get; set; }

        [Display(Name = "متن کپی رایت")]
        public string CopyRight { get; set; }


        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "توضیح")]
        public string Description { get; set; }

    }
}
