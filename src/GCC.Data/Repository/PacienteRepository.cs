using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(GCCContext context) : base(context)
        {
        }

        public async Task<Paciente> ObtenhaPaciente(Guid id)
        {
            return await Db.Pacientes.AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente> ObtenhaPorIdIdentity(Guid id)
        {
            return await Db.Pacientes.AsNoTracking()
                              .FirstOrDefaultAsync(p => Equals(p.UsuarioId, id));
        }
    }
}
