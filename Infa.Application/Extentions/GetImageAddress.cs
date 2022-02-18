using Infa.Application.Utils;
using Infa.Domain.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Extentions
{
    public static class GetImageAddress
    {
        public static string GetSliderImageAddress(this Slider slider)
        {
            return FilePath.SlideOriginPath + slider.ImageName;
        }

        public static string GetBannerImageAddress(this SiteBanner banner)
        {
            return FilePath.BannerOriginPath + banner.ImageName;
        }
    }
}