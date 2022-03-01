using Infa.Application.Interfaces;
using Infa.Application.Services;
using Infa.Data.Repositories;
using Infa.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SendEmail;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Http;
using GoogleReCaptcha.V3.Interface;
using GoogleReCaptcha.V3;
using Microsoft.AspNetCore.Http;

namespace Infa.IoC
{
    public static class ServiceContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region Services

            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IContactServices, ContactServices>();

            services.AddScoped<ISiteServices, SiteServices>();

            services.AddScoped<ISellerServices, SellerServices>();

            services.AddScoped<IProductServices, ProductServices>();

            #endregion


            #region Repositories

            services.AddScoped<IUserRepositories, UserRepositories>();

            services.AddScoped<IContactRepositories, ContactRepositories>();

            services.AddScoped<ISiteRepositories, SiteRepositories>();

            services.AddScoped<ISellerRepositories, SellerRepositories>();

            services.AddScoped<IProductRepositories, ProductRepositories>();

            #endregion


            #region Tools

            services.AddSingleton<HtmlEncoder>
                (HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

            services.AddScoped<IViewRenderService, RenderViewToString>();

            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion 

        }
    }
}