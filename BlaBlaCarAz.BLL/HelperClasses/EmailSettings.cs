using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.HelperClasses
{
  public  class EmailSettings
    {
        public const string EmailSetting = "EmailSettings";
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
