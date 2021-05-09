using GCC.Business.Modelos;
using System;
using System.Threading.Tasks;

namespace GCC.Business.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<Medico> ObtenhaMedico(Guid id);
        Task<Medico> ObtenhaMedicoPorCRM(CRM CRM);
    }
}
