using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.User.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SideBar");
        }
    }
}
