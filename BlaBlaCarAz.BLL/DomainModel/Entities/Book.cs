﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
   public class Book:EntityBase
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public DateTime Date { get; set; }
        public virtual Ride Ride { get; set; }
        public int RideId { get; set; }
    }
}
