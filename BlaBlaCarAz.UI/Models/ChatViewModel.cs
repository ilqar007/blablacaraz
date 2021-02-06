using BlaBlaCarAz.BLL.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Models
{
    public class ChatViewModel
    {
        public Chat Chat { get; set; }
        public bool IsSeen { get; set; }
    }
}
