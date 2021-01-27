using BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Controllers
{
    public class ProfileController :  BaseController
    {
        private readonly IService<BlaBlaCarAz.BLL.DomainModel.Entities.File> _service;
        public ProfileController(IService<BlaBlaCarAz.BLL.DomainModel.Entities.File> service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile thefile) {

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
                  await  _service.AddAsync(objfiles);
                }
            }


            return  new OkResult{ };
        }
    }
}
