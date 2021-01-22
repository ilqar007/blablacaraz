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

        [HttpPost]
        public async Task<IActionResult> Create(Ride model)
        {
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
                var result = await new System.Net.WebClient().DownloadStringTaskAsync(placeApiUrl);
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

    }
}
