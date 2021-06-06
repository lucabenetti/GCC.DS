using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCC.Data.Repository
{
    public class ConsultaRepository : Repository<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(GCCContext context) : base(context)
        {
        }

        public async Task<Consulta> ObtenhaConsulta(Guid id)
        {
            return await Db.Consultas.AsNoTracking()
                                     .Include(c => c.Medico)
                                     .Include(c => c.Paciente)
                                     .Include(c => c.Exame)
                                     .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Consulta>> ObtenhaConsultasMedico(Guid medicoId)
        {
            return await Db.Consultas.AsNoTracking()
                                     .Include(c => c.Medico)
                                     .Include(c => c.Paciente)
                                     .Where(c => c.Medico.Id == medicoId).ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> ObtenhaConsultasPaciente(Guid pacienteId)
        {
            return await Db.Consultas.AsNoTracking()
                                     .Include(c => c.Medico)
                                     .Include(c => c.Paciente)
                                     .Where(c => c.Paciente.Id == pacienteId).ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> ObtenhaConsultasMedicoPaciente()
        {
            return await Db.Consultas.AsNoTracking()
                                     .Include(c => c.Medico)
                                     .Include(c => c.Paciente)
                                     .ToListAsync();
        }
    }
}
