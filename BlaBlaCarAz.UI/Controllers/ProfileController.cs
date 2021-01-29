using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProfileController(IService<BlaBlaCarAz.BLL.DomainModel.Entities.File> service, IHostingEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;
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
                    await _service.AddAsync(objfiles);
                }
            }


            return new OkResult { };
        }


        [HttpGet]
        public async Task<IActionResult> GetImage(int? userId)
        {
            var user = !userId.HasValue? await GetAppUser():await GetAppUserById(userId.Value);
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
                string filePath =  Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);
                byte[] content = System.IO.File.ReadAllBytes(filePath);
                return File(content, "image/svg+xml", fileName);
            }


        }


    }
}
