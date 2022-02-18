using Infa.Data.Context;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Identity;
using Infa.Domain.Models.Site;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infa.Data.Repositories
{
    public partial class SiteRepositories : ISiteRepositories
    {
        private readonly MarketPlaceDBContext _context;

        public SiteRepositories(MarketPlaceDBContext context)
        {
            _context = context;
        }

    }
    public partial class SiteRepositories
    {
        public async Task<List<Slider>> GetAllActiveSliders()
        {
            return await _context.Slider.AsQueryable()
                .Where(a => a.IsActive && a.IsDeleted == false).ToListAsync();
        }

        public async Task<List<SiteBanner>> GetAllSiteBannerByPositions(List<SiteBannerPosition> siteBanners)
        {
            return await _context.SiteBanner.AsQueryable()
                .Where(s => siteBanners.Contains(s.SiteBannerPosition)).ToListAsync();
        }

        public async Task<AppSetting> GetAppSettingById(string id)
        {
            return await _context.AppSettings.AsQueryable()
               .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}