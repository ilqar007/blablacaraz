using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
    public class Message : EntityBase
    {
        public string Body { get; set; }
        public bool IsSeen { get; set; }
        public string Subject { get; set; }
        public virtual IList<AppUser> AppUsers { get; set; } = new List<AppUser>();
        public virtual Ride Ride { get; set; }
        public long? RideId { get; set; }

    }
}
