using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;

namespace GCC.Data.Repository
{
    public class ExameRepository : Repository<Exame>, IExameRepository
    {
        public ExameRepository(GCCContext context) : base(context)
        {
        }
    }
}
