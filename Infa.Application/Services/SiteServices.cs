using Infa.Application.Interfaces;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Site;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Services
{
    public partial class SiteServices : ISiteServices
    {
        private readonly ISiteRepositories _siteRepositories;
        private readonly IConfiguration _configuration;

        public SiteServices(
            ISiteRepositories siteRepositories,
            IConfiguration configuration
            )
        {
            _siteRepositories = siteRepositories;
            _configuration = configuration;
        }


    }

    public partial class SiteServices
    {
        public async Task<List<Slider>> GetAllActiveSliders()
        {
           return await _siteRepositories.GetAllActiveSliders();
        }

        public async Task<List<SiteBanner>> GetAllSitesBannerByPositions(List<SiteBannerPosition> siteBanners)
        {
            return await _siteRepositories.GetAllSiteBannerByPositions(siteBanners);
        }

        public async Task<AppSetting> GetAppSetting()
        {
            return await _siteRepositories.GetAppSettingById(_configuration.GetSection("GetAppSettingId")["AppSettingId"]);
        }
    }
}
