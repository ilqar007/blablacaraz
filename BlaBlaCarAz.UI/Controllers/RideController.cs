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
    [Authorize]
    public class RideController : Controller
    {
        private readonly IService<Ride> _rideService;
        private readonly UserManager<AppUser> _userManagerService;
        public RideController(IService<Ride> rideService, UserManager<AppUser> userManagerService)
        {
            _rideService = rideService;
            _userManagerService = userManagerService;

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ride model)
        {
            model.AppUser = await _userManagerService.GetUserAsync(User);
            await _rideService.AddAsync(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appUser = await _userManagerService.GetUserAsync(User);
            var rides = await _rideService.GetAllAsync(x => x.AppUserId == appUser.Id);
            return View(rides);
        }
    }
}
