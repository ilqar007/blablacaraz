using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IService<Message> _service;
        private readonly IService<Ride> _rideService;
        public MessageController(IService<Message> service, IService<Ride> rideService)
        {
            _service = service;
            _rideService = rideService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appUser = await GetAppUser();
            var messages = await _service.GetAllAsync(x => x.AppUsers.Any(u => u.Id == appUser.Id));
            var rides = messages.GroupBy(x => x.Ride).Select(x => x.Key);
            return View(rides);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int rideId)
        {
            var ride = await _rideService.FindAsync(rideId);
            if (ride == null)
                return RedirectToAction("Index", "Home");
            var messages = new List<Message>();
            messages.Add(new Message { Ride = ride });
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Message model)
        {
            var ride = await _rideService.FindAsync(model.RideId);
            if (ride == null)
                return RedirectToAction("Index", "Home");

            var fromUser = await GetAppUser();
            var toUser = await GetAppUserById(ride.AppUserId);
            model.IsSeen = false;
            model.AppUsers.Add(fromUser);
            model.AppUsers.Add(toUser);
            model.CreatedOn = DateTime.Now;
            await _service.AddAsync(model);
            return RedirectToAction(nameof(Show), new { rideId = model.RideId });
        }

        [HttpGet]
        public async Task<IActionResult> Show(int rideId)
        {
            var ride = await _rideService.FindAsync(rideId);
            if (ride == null)
                return RedirectToAction("Index", "Home");

            var messages = await _service.GetAllAsync(x => x.RideId == rideId);
            return View(nameof(Create), messages);
        }

    }
}
