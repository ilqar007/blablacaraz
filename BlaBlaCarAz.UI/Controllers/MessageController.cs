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
        private readonly IService<Chat> _chatService;
        private readonly IService<Book> _bookService;

        public MessageController(IService<Message> service, IService<Ride> rideService, IService<Chat> chatService, IService<Book> bookService)
        {
            _service = service;
            _rideService = rideService;
            _chatService = chatService;
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appUser = await GetAppUser();
            var chats = await _chatService.GetAllAsync(x => x.Messages.Any(m => m.ToUserId == appUser.Id || m.FromUserId == appUser.Id));
            ViewBag.AppUserId = appUser.Id;
            return View(chats);
        }


        [HttpGet]
        public async Task<IActionResult> Create(int rideId)
        {
            var fromUser = await GetAppUser();
            var ride = await _rideService.FindAsync(rideId);
            if (ride == null || ride.AppUserId == fromUser.Id)
                return RedirectToAction("Index", "Home");

            return View(new Chat { Ride = ride });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Chat model)
        {
            var fromUser = await GetAppUser();
            var ride = await _rideService.FindAsync(model.Ride.Id);
            if (ride == null)
                return RedirectToAction("Index", "Home");


            var chat = await _chatService.FindAsync(model.Id);
            if (chat == null)
            {
                chat = new Chat { Ride = ride, CreatedOn = DateTime.Now, AppUser = fromUser };
            }
            if ((chat.Id > 0 && !chat.Messages.Any(x => x.FromUserId == fromUser.Id || x.ToUserId == fromUser.Id)) || chat.Ride.Id != ride.Id)
                return RedirectToAction("Index", "Home");

            long toUserId = chat.Id > 0 && ride.AppUserId == fromUser.Id ? chat.AppUserId : ride.AppUserId;
            var toUser = await GetAppUserById(toUserId);
            var message = new Message();
            message.IsSeen = false;
            message.FromUser = fromUser;
            message.ToUser = toUser;
            message.CreatedOn = DateTime.Now;
            message.Body = model.Messages[0].Body;
            if (chat.Id == 0)
            {
                chat.Messages.Add(message);
                await _chatService.AddAsync(chat);
            }
            else
            {
                message.Chat = chat;
                await _service.AddAsync(message);
            }
            return RedirectToAction(nameof(Show), new { id = chat.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Show(long id)
        {
            var appUser = await GetAppUser();
            var chat = await _chatService.FindAsync(id);
            if (chat == null || !chat.Messages.Any(x => x.FromUserId == appUser.Id || x.ToUserId == appUser.Id))
                return RedirectToAction("Index", "Home");

            var unreadMessages = chat.Messages.Where(x => x.ToUserId == appUser.Id && !x.IsSeen);
            if (unreadMessages.Count() > 0)
            {
                foreach (var item in unreadMessages)
                {
                    item.IsSeen = true;
                }
                await _service.UpdateRangeAsync(unreadMessages);
            }
            return View(nameof(Create), chat);
        }


        [HttpGet]
        public async Task<IActionResult> GetNewMessageCount()
        {
            var fromUser = await GetAppUser();
            var unreadMessages = await _service.GetAllAsync(x => x.ToUserId == fromUser.Id && !x.IsSeen);
            var unconfirmedBooksCount = await _bookService.CountAsync(x => x.Ride.AppUserId == fromUser.Id && !x.IsConfirmed);
            return new ObjectResult(new { MessageCount = unreadMessages.Count, BookRequestCount = unconfirmedBooksCount });
        }

    }
}
