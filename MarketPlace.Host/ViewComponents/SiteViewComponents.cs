using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.Models.Site;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.ViewComponents
{

    public class SiteHeaderViewComponent : ViewComponent
    {
        private readonly IUserServices _userServices;

        public SiteHeaderViewComponent(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userServices.GetUserAsync(UserClaimsPrincipal.GetUserId());
            return View("SiteHeader",user);
        }
    }
    public class SiteFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }
    public class SiteSliderViewComponent : ViewComponent
    {
        private readonly ISiteServices _siteServices;

        public SiteSliderViewComponent(ISiteServices siteServices)
        {
            _siteServices = siteServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _siteServices.GetAllActiveSliders();
            return View("SiteSlider",sliders);
        }
    }

}
