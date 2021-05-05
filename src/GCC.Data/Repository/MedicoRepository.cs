using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GCC.Data.Repository
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(GCCContext context) : base(context)
        {
        }

        public async Task<Medico> ObtenhaMedico(Guid id)
        {
            return await Db.Medicos.AsNoTracking()
                                     .Include(m => m.Especialidade)
                                     .Include(m => m.JornadaDeTrabalho)
                                     .Include(m => m.CRM)
                                     .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
