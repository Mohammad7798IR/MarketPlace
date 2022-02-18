﻿using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Controllers
{
    public class BaseController : Controller
    {
        protected string ErrorMessage   = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage    = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
