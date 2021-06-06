using GCC.Business.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Business.Interfaces
{
    public interface IExameRepository : IRepository<Exame>
    {
        Task<bool> JaCadastradoMesmoNome(string nome);
        Task<Exame> ObterExame(Guid id);
    }
}
