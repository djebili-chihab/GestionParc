
using GestionNonconformite_NCARouiba.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionNonconformite_NCARouiba.Models
{
    public class AppDbContext : IdentityDbContext<User,Role,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }*/

        public DbSet<DemandeCarburant> demandeCarburants { get; set; }
        public DbSet<DemandeEntretien> demandeEntretiens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> cars { get; set; }
    }
}
