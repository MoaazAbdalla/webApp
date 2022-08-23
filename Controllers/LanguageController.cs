using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MyCreate.Migrations;
using MyCreate.model;

namespace MyCreate.Controllers
{

    [Route("languages")]
    public class LanguageController : Controller
    {
        [Route("change")]

        public IActionResult Change(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
                );
            return RedirectToAction("index","home");
        }
    }
}
