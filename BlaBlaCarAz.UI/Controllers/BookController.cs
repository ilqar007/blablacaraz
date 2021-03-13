using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using BlaBlaCarAz.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class BookController : BaseController
    {
        private readonly IService<Book> _bookService;
        private readonly IService<Ride> _rideService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public BookController(IService<Book> bookService, IService<Ride> rideService, IStringLocalizer<SharedResource> localizer)
        {
            _bookService = bookService;
            _rideService = rideService;
            _localizer = localizer;
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
            if (ride == null || ride.AppUserId == appUser.Id)
                return RedirectToAction("Index", "Home");

            if (ride.LoadLimits - ride.Books.Where(x => x.IsConfirmed).Sum(x => x.LoadLimits) < loadLimits)
                return RedirectToAction("Index", "Home");

            var book = new Book
            {
                AppUser = appUser,
                CreatedOn = DateTime.Now,
                LoadLimits = loadLimits,
                IsConfirmed = ride.CanBookInstantly,
                Ride = ride
            };
            ride.Books.Add(book);
            await _rideService.UpdateAsync(ride);
            if (book.IsConfirmed)
            {
                await SendBookConfirmEmail(book);
            }
            else
            {
                await SendBookRequestEmail(ride);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task SendBookConfirmEmail(Book book)
        {
            await SendEmail(book.AppUser.Email, _localizer[nameof(SharedResource.PaymentConfirmationEmailSubject)], string.Format(_localizer[nameof(SharedResource.PaymentConfirmationEmailBody)], book.LoadLimits,
             book.Ride.LoadType,
             book.CreatedOn.Value.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
             book.Ride.From,
             book.Ride.To,
             book.LoadLimits * book.Ride.Price,
             book.LoadLimits * book.Ride.Price,
             book.CreatedOn.Value.ToString("dd/MM/yy")
             )); 
        }
        private async Task SendBookRequestEmail(Ride ride)
        {
            await SendEmail(ride.AppUser.Email, _localizer[nameof(SharedResource.ConfirmBookRequestEmailSubject)], $"{_localizer[nameof(SharedResource.ConfirmBookRequestEmailBody)]} \n {ride.From} \n {ride.To} \n {ride.Date.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");

        }
        [HttpGet]
        public async Task<IActionResult> Confirm(int rideId, int bookId)
        {
            var appUser = await GetAppUser();
            var ride = await _rideService.GetSingleAsync(x => x.Id == rideId && x.AppUserId == appUser.Id);
            if (ride == null)
                return RedirectToAction("Index", "Home");

            var book = ride.Books.Where(x => x.Id == bookId).FirstOrDefault();
            if (book == null || book.IsConfirmed)
                return RedirectToAction("Index", "Home");

            if (ride.LoadLimits - ride.Books.Where(x => x.IsConfirmed).Sum(x => x.LoadLimits) < book.LoadLimits)
                return RedirectToAction("Index", "Home");

            book.IsConfirmed = true;
            await _rideService.UpdateAsync(ride);
            await SendBookConfirmEmail(book);

            return RedirectToAction("BookConfirmations", "Ride");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appUser = await GetAppUser();
            var book = await _bookService.GetSingleAsync(x => x.Id == id && x.AppUserId == appUser.Id);
            if (book != null && !book.IsConfirmed && book.CreatedOn.Value.AddDays(1) < book.Ride.Date)
            {
                await _bookService.DeleteAsync(book);
            }
            return RedirectToAction("Index");
        }

    }
}
