using BlaBlaCarAz.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Enums
{
    public enum LoadType
    {
        [Display(Name = "Document", ResourceType = typeof(SharedResource))]
        Document,
        [Display(Name = "Bag", ResourceType = typeof(SharedResource))]
        Bag
    }
}
