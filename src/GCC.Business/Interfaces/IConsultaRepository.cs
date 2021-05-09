using GCC.Business.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Business.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        Task<IEnumerable<Consulta>> ObtenhaConsultasPaciente(Guid pacienteId);
        Task<IEnumerable<Consulta>> ObtenhaConsultasMedico(Guid medicoId);
        Task<Consulta> ObtenhaConsulta(Guid id);
        Task<IEnumerable<Consulta>> ObtenhaConsultasMedicoPaciente();
    }
}
