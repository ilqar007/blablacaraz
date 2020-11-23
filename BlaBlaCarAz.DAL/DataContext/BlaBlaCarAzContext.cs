using BlaBlaCarAz.BLL.DomainModel.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlaBlaCarAz.DAL.DataContext
{
    public class BlaBlaCarAzContext :  IdentityDbContext<AppUser>
    {
        public BlaBlaCarAzContext()
        {

        }

        public BlaBlaCarAzContext(DbContextOptions<BlaBlaCarAzContext> options) : base(options)
        {
        }

        public virtual DbSet<Ride> Rides { get; set; }


    }
}
