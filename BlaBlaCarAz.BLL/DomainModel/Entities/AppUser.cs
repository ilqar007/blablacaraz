using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
   public class AppUser : IdentityUser<long>
    {
        public virtual IList<Ride> Rides { get; set; }
        public virtual IList<File> Files { get; set; }
        public virtual IList<Chat> Chats { get; set; }

    }
}
