using BlaBlaCarAz.BLL.DomainModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static BlaBlaCarAz.BLL.DomainModel.Entities.IdentityModels;

namespace BlaBlaCarAz.DAL.DataContext
{
    public class BlaBlaCarAzContext : IdentityDbContext<AppUser, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
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
        public virtual DbSet<Log> Logs { get; set; }
        

    }
}
