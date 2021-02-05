using BlaBlaCarAz.BLL.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Models
{
    public class MessageViewModel
    {
        public Message Message { get; set; }
        public Ride Ride { get; set; }
    }
}
