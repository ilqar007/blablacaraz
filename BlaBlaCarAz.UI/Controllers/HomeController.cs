using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.UI.Models;
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
            var rides = await _rideService.GetAllAsync(x => x.From == model.From && x.To == model.To && x.Date.Date == model.Date.Date);
            return View(rides);
        }
    }
}
