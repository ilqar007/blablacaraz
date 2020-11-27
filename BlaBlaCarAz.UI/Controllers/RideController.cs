using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class RideController : BaseController
    {
        private readonly IService<Ride> _rideService;
        public RideController(IService<Ride> rideService)
        {
            _rideService = rideService;
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
      
    }
}
