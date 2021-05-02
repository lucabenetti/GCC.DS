using GCC.Business.Modelos;
using GCC.Business.Modelos.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Data.Context
{
    public class GCCContext : DbContext
    {
        DbSet<Consulta> Consultas { get; set; }
        DbSet<CRM> CRMs { get; set; }
        DbSet<DiaDeTrabalho> DiasDeTrabalho { get; set; }
        DbSet<Especialidade> Especialidades { get; set; }
        DbSet<Medico> Medicos { get; set; }
        DbSet<Paciente> Pacientes { get; set; }
        DbSet<Prontuario> Prontuarios { get; set; }
        DbSet<Secretaria> Secretarias { get; set; }

        public GCCContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GCCContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            var usuarioEntidade = modelBuilder.Entity<Usuario>();
            usuarioEntidade.ToTable("AspNetUsers");

            base.OnModelCreating(modelBuilder);
        }
    }
}
