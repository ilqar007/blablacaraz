using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
   public class AppUser : IdentityUser<long>
    {
        public virtual IList<Ride> Rides { get; set; }
    }
}
