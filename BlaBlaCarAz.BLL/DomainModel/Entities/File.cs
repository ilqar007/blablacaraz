using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
   public class File : EntityBase
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        [MaxLength]
        public byte[] DataFiles { get; set; }
        public long AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
