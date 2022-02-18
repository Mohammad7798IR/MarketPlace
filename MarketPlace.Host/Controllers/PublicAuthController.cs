using GoogleReCaptcha.V3.Interface;
using Infa.Application.Interfaces;
using Infa.Domain.Models.Identity;
using Infa.Domain.ViewModels.PublicAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.Host.Controllers
{

    public partial class PublicAuthController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IConfiguration _configuration;

        public PublicAuthController(
            IUserServices userServices
            , ICaptchaValidator captchaValidator,
            IConfiguration configuration)
        {
            _userServices = userServices;
            _captchaValidator = captchaValidator;
            _configuration = configuration;
        }
    }


    public partial class PublicAuthController
    {

        #region Register

        [Route("Register")]
        public IActionResult Register()

        {
            return View();
        }


        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserVM userVM)
        {

            if (await _captchaValidator.IsCaptchaPassedAsync(userVM.Token))
            {
                if (ModelState.IsValid)
                {
                    var result = await _userServices.Register(userVM);
                    switch (result)
                    {
                        case RegisterUserResult.Success:
                            TempData[SuccessMessage] = "ثبت نام با موفقیت انجام شد .";
                            TempData[InfoMessage] = "ایمیل فعال سازی ارسال شد ";
                            ViewBag.IsSuccess = true;
                            return Redirect("/");

                        case RegisterUserResult.EmailExists:
                            TempData[ErrorMessage] = "ایمیل قبلا ثبت شده است";
                            ViewBag.IsSuccess = false;
                            return View(userVM);

                        case RegisterUserResult.Fail:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            ViewBag.IsSuccess = false;
                            return View(userVM);

                        default:
                            break;
                    }
                }
            }
            TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            return View(userVM);


            return View();
        }

        #endregion


        #region Login

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserVM userVM)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(userVM.Token))
            {
                if (ModelState.IsValid)
                {
                    var result = await _userServices.Login(userVM);

                    switch (result)
                    {
                        case LoginUserResult.Success:

                            var user = _userServices.GetByEmail(userVM.Email).Result;


                            var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name , user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString())
                        };


                            foreach (var item in user.UserRoles)
                            {
                                if (item.RoleId == _configuration.GetSection("Roles")["AplicationUser"])
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, "AplicationUser"));
                                }
                                if (item.RoleId == _configuration.GetSection("Roles")["AplicationAdmin"])
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, "AplicationAdmin"));
                                }
                                if (item.RoleId == _configuration.GetSection("Roles")["AplicationSeller"])
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, "AplicationSeller"));
                                }
                            }

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);


                            var properties = new AuthenticationProperties
                            {
                                IsPersistent = userVM.RememberMe,
                            };


                            await HttpContext.SignInAsync(principal, properties);

                            TempData[SuccessMessage] = "ورود با موفقیت انجام شد";

                            ViewBag.IsSuccess = true;

                            return Redirect("/");

                        case LoginUserResult.InActive:
                            TempData[ErrorMessage] = "حساب کاربری شما فعال نشده است";
                            ViewBag.IsSuccess = false;
                            return View(userVM);


                        case LoginUserResult.NotFound:
                            TempData[ErrorMessage] = "ایمیل یا رمز عبور صحیح نمی باشد";
                            ViewBag.IsSuccess = false;
                            return View(userVM);


                        default:
                            break;
                    }

                }
            }
            TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            return View();
        }


        #endregion


        #region LogOut

        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion


        #region ActiveAccount

        [HttpGet]
        public async Task<IActionResult> ActiveAccount(string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.ActiveAccount(Id);

                switch (result)
                {
                    case ActiveAccountResult.Success:
                        TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد ";
                        return Redirect("/");


                    case ActiveAccountResult.NotFound:
                        return NotFound();

                }
            }
            return View();
        }


        #endregion


        //Sends Email To Reset Passwords
        #region ForgotPassword


        [HttpGet]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordUserVM userVM)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(userVM.Token))
            {
                if (ModelState.IsValid)
                {
                    var result = await _userServices.ForgotPassword(userVM);

                    switch (result)
                    {
                        case ForgotPasswordResult.Success:
                            TempData[SuccessMessage] = "ایمیل تغییر رمز عبور ارسال شد";
                            return Redirect("/login");


                        case ForgotPasswordResult.NotFound:
                            TempData[ErrorMessage] = "ایمیل وارد شده یافت نشد";
                            ViewBag.IsSuccess = false;
                            return View(userVM);

                        case ForgotPasswordResult.InActive:
                            TempData[ErrorMessage] = "حساب کاربری شما فعال نشده است";
                            ViewBag.IsSuccess = false;
                            return View(userVM);


                        default:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            ViewBag.IsSuccess = false;
                            return View(userVM);
                    }
                }
            }
            TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            return View(userVM);
        }

        #endregion


        #region ResetPassword


        [HttpGet]
        public async Task<IActionResult> ResetPassword(string Id)
        {
            if (await _userServices.ExitsByActiveCode(Id) != true)
            {
                return NotFound();
            }


            ResetPasswordUserVM userVM = new ResetPasswordUserVM()
            {
                ActiveCode = Id
            };

            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordUserVM userVM)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(userVM.Token))
            {

                if (ModelState.IsValid)
                {

                    var result = await _userServices.ResetPassword(userVM);



                    switch (result)
                    {
                        case ResetPasswordResult.Success:
                            TempData[SuccessMessage] = " رمز عبور شما با موفقیت تغییر یافت";
                            return Redirect("/login");
                        case ResetPasswordResult.Fail:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            ViewBag.IsSuccess = false;
                            return View();
                        default:
                            break;
                    }
                }
            }
            TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            return View(userVM);
        }


        #endregion

    }


    public partial class PublicAuthController
    {

        [Route("test")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            return View();
        }
    }
}
