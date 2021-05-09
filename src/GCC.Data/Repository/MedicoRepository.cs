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
                                     .Include(m => m.JornadaDeTrabalho)
                                     .Include(m => m.CRM)
                                     .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Medico> ObtenhaMedicoPorCRM(CRM CRM)
        {
            return await Db.Medicos.AsNoTracking()
                                     .Include(m => m.JornadaDeTrabalho)
                                     .Include(m => m.CRM)
                                     .FirstOrDefaultAsync(m => m.CRM.Numero == CRM.Numero &&
                                                               m.CRM.UF == CRM.UF);
        }
    }
}
