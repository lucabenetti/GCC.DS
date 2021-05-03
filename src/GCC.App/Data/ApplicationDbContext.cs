using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GCC.App.ViewModels;

namespace GCC.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<GCC.App.ViewModels.MedicoViewModel> MedicoViewModel { get; set; }

        public DbSet<GCC.App.ViewModels.PacienteViewModel> PacienteViewModel { get; set; }

        public DbSet<GCC.App.ViewModels.ConsultaViewModel> ConsultaViewModel { get; set; }
    }
}
