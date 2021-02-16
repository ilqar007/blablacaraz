using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
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
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected async Task<AppUser> GetAppUser()
        {
            var userManagerService = (UserManager<AppUser>)Request.HttpContext.RequestServices.GetService(typeof(UserManager<AppUser>));
            return await userManagerService.GetUserAsync(User);
        }

        protected async Task<AppUser> GetAppUserById(long id)
        {
            var userManagerService = (UserManager<AppUser>)Request.HttpContext.RequestServices.GetService(typeof(UserManager<AppUser>));
            return await userManagerService.FindByIdAsync(id.ToString());
        }

        protected async Task SendEmail(string email, string subject, string htmlMessage)
        {
            var _emailSender = (IEmailSender)Request.HttpContext.RequestServices.GetService(typeof(IEmailSender));
            await _emailSender.SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
