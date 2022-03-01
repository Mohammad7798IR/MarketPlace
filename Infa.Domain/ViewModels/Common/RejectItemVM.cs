using System.ComponentModel.DataAnnotations;

namespace Infa.Domain.ViewModels.Common
{
    public class RejectItemVM
    {
        public long Code { get; set; }

        [Display(Name = "توضیحات عدم تایید اطلاعات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RejectMessage { get; set; }
    }
}
