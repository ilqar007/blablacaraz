using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.HelperClasses;
using BlaBlaCarAz.BLL.ServiceLayer.DtoAndMessages.GooglePlacesApi;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class RideController : BaseController
    {
        private readonly IService<Ride> _rideService;
        private readonly GooglePlacesSettings _googlePlacesSettings;
        public RideController(IService<Ride> rideService, IOptions<GooglePlacesSettings> googlePlacesSettings)
        {
            _rideService = rideService;
            _googlePlacesSettings = googlePlacesSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appUser = await GetAppUser();
            var ride = await _rideService.GetSingleAsync(x => x.Id == id && x.AppUserId == appUser.Id);
            if (ride != null && ride.Books.Count == 0)
            {
                await _rideService.DeleteAsync(ride);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ride model)
        {
            if (model.Id > 0)
                return RedirectToAction("Index", "Home");

            model.AppUser = await GetAppUser();
            await _rideService.AddAsync(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appUser = await GetAppUser();
            var rides = await _rideService.GetAllAsync(x => x.AppUserId == appUser.Id);
            return View(rides);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<JsonResult> GetEventVenuesList(string SearchText)
        {
            string placeApiUrl = _googlePlacesSettings.GooglePlaceAPIUrl;

            try
            {
                placeApiUrl = placeApiUrl.Replace("{0}", SearchText);
                string result = string.Empty;
                using (var client = new System.Net.WebClient())
                {
                    result = await client.DownloadStringTaskAsync(placeApiUrl);
                }
                var Jsonobject = JsonConvert.DeserializeObject<RootObject>(result);

                List<Suggestion> list = Jsonobject.suggestions;
                if (list.Any(s => s.matchLevel == "city"))
                    list = list.Where(s => s.matchLevel == "city").ToList();
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Departure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Departure(Ride model)
        {
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public IActionResult Arrival(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult DepartureDate(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult DepartureTime(Ride model)
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult DepartureFlightNumber(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult LoadType(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult LoadLimits(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Approval(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult PriceRecommendation(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult PriceSetting(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult ProfilePicture(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult ProfilePictureChoice(Ride model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Comment(Ride model)
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id, int loadLimits)
        {
            var ride = await _rideService.FindAsync(id);
            if (ride == null)
                return RedirectToAction("Index", "Home");
            if (ride.LoadLimits - ride.Books.Where(x => x.IsConfirmed).Sum(x => x.LoadLimits) < loadLimits)
                return RedirectToAction("Index", "Home");


            return View(ride);
        }


        [HttpGet]
        public async Task<IActionResult> BookConfirmations()
        {
            var appUser = await GetAppUser();
            var rides = await _rideService.GetAllAsync(x => x.AppUserId == appUser.Id);
            return View(rides);
        }
    }
}
