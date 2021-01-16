using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class BookController : BaseController
    {
        private readonly IService<Book> _bookService;
        private readonly IService<Ride> _rideService;


        public BookController(IService<Book> bookService, IService<Ride> rideService)
        {
            _bookService = bookService;
            _rideService = rideService;
        }

        public async Task<IActionResult> Index()
        {
            var appUser = await GetAppUser();
            var books = await _bookService.GetAllAsync(x=>x.AppUserId == appUser.Id);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int rideId)
        {
            //github test
            var appUser = await GetAppUser();
            var ride = await _rideService.FindAsync(rideId);
            if (ride == null)
                throw new Exception("No such ride");
            var book = new Book
            {
                AppUser = appUser,
                Date = DateTime.Now,
                Ride = ride
            };
            await _bookService.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }

    }
}
