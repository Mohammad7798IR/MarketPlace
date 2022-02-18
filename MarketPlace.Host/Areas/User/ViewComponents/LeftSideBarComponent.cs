using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.User.ViewComponents
{
    public class LeftSideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("LeftSideBar");
        }
    }
}
