using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GCC.Data.Repository
{
    public class SecretariaRepository : Repository<Secretaria>, ISecretariaRepository
    {
        public SecretariaRepository(GCCContext context) : base(context)
        {
        }
    }
}
