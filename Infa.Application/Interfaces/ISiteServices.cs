using Infa.Domain.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public interface ISiteServices
    {
        Task<List<Slider>> GetAllActiveSliders();

        Task<List<SiteBanner>> GetAllSitesBannerByPositions(List<SiteBannerPosition> siteBanners);

        Task<AppSetting> GetAppSetting();
    }
}
