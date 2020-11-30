using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
   public class Ride:EntityBase
    {
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int PassengerCount { get; set; }
        public long AppUserId { get; set; }
        
        public virtual AppUser AppUser { get; set; }

        public decimal Price { get; set; }

        public virtual IList<Book> Books { get; set; }
    }
}
