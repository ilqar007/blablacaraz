using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
    public class Chat : EntityBase
    {
        public virtual IList<Message> Messages { get; set; } = new List<Message>();
        public virtual Ride Ride { get; set; }
        public long RideId { get; set; }
        public long AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
