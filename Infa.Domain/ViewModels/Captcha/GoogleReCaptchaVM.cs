using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Captcha
{
	public class GoogleReCaptchaVM
	{
		[Required(ErrorMessage = "لطفا {0} وارد کنید")]
		public string Token { get; set; }
	}
}
