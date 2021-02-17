using BlaBlaCarAz.BLL.DomainModel.Entities;
using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IService<BlaBlaCarAz.BLL.DomainModel.Entities.File> _service;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<AppUser> _userManager;


        public ProfileController(IService<BlaBlaCarAz.BLL.DomainModel.Entities.File> service, IWebHostEnvironment hostingEnvironment, UserManager<AppUser> userManager)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile thefile)
        {

            if (thefile != null)
            {
                if (thefile.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(thefile.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new BlaBlaCarAz.BLL.DomainModel.Entities.File()
                    {
                        Name = newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now,
                        AppUser = await GetAppUser()
                    };

                    using (var target = new MemoryStream())
                    {
                        thefile.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }
                    var files = await _service.GetAllAsync(x => x.AppUserId == objfiles.AppUser.Id);
                    await _service.DeleteRangeAsync(files, false);
                    await _service.AddAsync(objfiles);
                }
            }


            return new OkResult { };
        }


        [HttpGet]
        public async Task<IActionResult> GetImage(long? userId)
        {
            var user = !userId.HasValue ? await GetAppUser() : await GetAppUserById(userId.Value);
            var profileImages = await _service.GetAllAsync(x => x.AppUserId == user.Id);
            var image = profileImages.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            if (image != null)
            {
                byte[] content = image.DataFiles;
                return File(content, "image/png", image.Name);
            }
            else
            {
                string fileName = "passenger-m-02.svg";
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);
                byte[] content = System.IO.File.ReadAllBytes(filePath);
                return File(content, "image/svg+xml", fileName);
            }


        }

        [HttpGet]
        public async Task<IActionResult> Show(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return RedirectToAction("Index", "Home");


            return View(user);
        }


    }
}
