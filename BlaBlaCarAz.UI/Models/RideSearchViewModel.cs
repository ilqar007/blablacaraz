using BlaBlaCarAz.BLL.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Models
{
    public class RideSearchViewModel
    {
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public LoadType LoadType { get; set; }
        public int LoadLimits { get; set; } = 1;
    }
}
