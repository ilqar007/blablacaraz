using BlaBlaCarAz.BLL.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlaBlaCarAz.UI.Models
{
    public class RideSearchResultViewModel
    {
        public RideSearchViewModel searchModel { get; set; }
        public IList<Ride> Rides { get; set; }
    }
}
