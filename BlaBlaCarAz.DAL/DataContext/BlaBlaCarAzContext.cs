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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<Book> Books { get; set; }


    }
}
