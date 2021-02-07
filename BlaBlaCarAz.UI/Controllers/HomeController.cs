using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Ride> _rideService;

        public HomeController(ILogger<HomeController> logger, IService<Ride> rideService)
        {
            _logger = logger;
            _rideService = rideService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> RideSearch(RideSearchViewModel model)
        {
            var rides = await _rideService.GetAllAsync(x => x.From == model.From && x.To == model.To && x.Date.Date >= DateTime.Now.Date && x.Date.Date == model.Date.Date && x.LoadType == model.LoadType && ((x.LoadLimits - x.Books.Where(x=>x.IsConfirmed).DefaultIfEmpty().Sum(b => b.LoadLimits)) >= model.LoadLimits));
            var resultModel = new RideSearchResultViewModel { searchModel=model, Rides=rides};
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1),IsEssential=true, }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
