using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public IActionResult Error(int StatusCode)
        {

            switch (StatusCode)
            {
                case 404:
                    return View("NotFound");
                default:
                    break;
            }
            return View();
        }
    }
}
