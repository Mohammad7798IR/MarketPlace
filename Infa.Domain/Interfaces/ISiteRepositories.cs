using Infa.Domain.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Interfaces
{
    public interface ISiteRepositories
    {
        public Task<List<Slider>> GetAllActiveSliders();

        public Task<List<SiteBanner>> GetAllSiteBannerByPositions(List<SiteBannerPosition> siteBanners);

        Task<AppSetting> GetAppSettingById(string id);
    }
}
