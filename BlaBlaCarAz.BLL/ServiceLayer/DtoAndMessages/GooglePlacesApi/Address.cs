using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.ServiceLayer.DtoAndMessages.GooglePlacesApi
{
    public class Address
    {
        public string country { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public override string ToString()
        {
            //return string.Format("{0},{1},{2},{3},{4},{5},{6}",);
            return (!string.IsNullOrWhiteSpace(country) ? country + ", " : "") + (!string.IsNullOrWhiteSpace(state) ? state + ", " : "") + (!string.IsNullOrWhiteSpace(county) ? county + ", " : "")  + (!string.IsNullOrWhiteSpace(city) ? city + ", " : "") + (!string.IsNullOrWhiteSpace(district) ? district + ", " : "") + (!string.IsNullOrWhiteSpace(street) ? street + ", " : "") + (!string.IsNullOrWhiteSpace(postalCode) ? postalCode + ", " : "");
        }
    }
}