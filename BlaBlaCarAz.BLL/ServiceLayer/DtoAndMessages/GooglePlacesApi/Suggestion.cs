using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.ServiceLayer.DtoAndMessages.GooglePlacesApi
{
    public class Suggestion
    {
        public string label { get; set; }
        public string language { get; set; }
        public string countryCode { get; set; }
        public string locationId { get; set; }
        public Address address { get; set; }
        public string matchLevel { get; set; }
        public string addressCombined { get { return address.ToString(); } }
    }
}
