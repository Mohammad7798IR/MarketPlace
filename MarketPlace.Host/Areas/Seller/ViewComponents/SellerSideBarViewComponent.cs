using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Seller.ViewComponents
{
    public class SellerSideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SellerSideBar");
        }
    }
}
