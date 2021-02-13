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
            var books = await _bookService.GetAllAsync(x => x.AppUserId == appUser.Id);
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int rideId, int loadLimits)
        {
            var appUser = await GetAppUser();
            var ride = await _rideService.GetSingleAsync(x => x.Id == rideId);
            if (ride == null)
                return RedirectToAction("Index", "Home");

            if (ride.LoadLimits - ride.Books.Where(x => x.IsConfirmed).Sum(x => x.LoadLimits) < loadLimits)
                return RedirectToAction("Index", "Home");

            var book = new Book
            {
                AppUser = appUser,
                Date = DateTime.Now,
                LoadLimits = loadLimits,
                IsConfirmed = ride.CanBookInstantly,
                Ride = ride
            };
            ride.Books.Add(book);
            await _rideService.UpdateAsync(ride);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Confirm(int rideId, int bookId)
        {
            var appUser = await GetAppUser();
            var ride = await _rideService.GetSingleAsync(x => x.Id == rideId && x.AppUserId == appUser.Id);
            if (ride == null)
                return RedirectToAction("Index", "Home");

            var book = ride.Books.Where(x => x.Id == bookId).FirstOrDefault();
            if (book == null)
                return RedirectToAction("Index", "Home");

            if (ride.LoadLimits - ride.Books.Where(x => x.IsConfirmed).Sum(x => x.LoadLimits) < book.LoadLimits)
                return RedirectToAction("Index", "Home");

            book.IsConfirmed = true;
            await _rideService.UpdateAsync(ride);
            return RedirectToAction("BookConfirmations", "Ride");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appUser = await GetAppUser();
            var book = await _bookService.GetSingleAsync(x => x.Id == id && x.AppUserId == appUser.Id);
            if (book != null && book.Date.AddDays(1) < book.Ride.Date)
            {
                await _bookService.DeleteAsync(book);
            }
            return RedirectToAction("Index");
        }

    }
}
