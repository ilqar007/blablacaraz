using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
    public class Message : EntityBase
    {
        public string Body { get; set; }
        public bool IsSeen { get; set; }       


        public long FromUserId { get; set; }
        [ForeignKey(nameof(FromUserId))]
        public virtual AppUser FromUser { get; set; }

        public long ToUserId { get; set; }
        [ForeignKey(nameof(ToUserId))]
        public virtual AppUser ToUser { get; set; }

        public virtual Chat Chat { get; set; }
        public long ChatId { get; set; }
    }
}
