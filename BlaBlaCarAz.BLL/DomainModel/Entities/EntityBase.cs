using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlaBlaCarAz.BLL.DomainModel.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }
    }
}
