﻿using GCC.Business.Interfaces;
using GCC.Business.Modelos;
using GCC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GCC.Data.Repository
{
    public class ExameRepository : Repository<Exame>, IExameRepository
    {
        public ExameRepository(GCCContext context) : base(context)
        {
        }

        public async Task<bool> JaCadastradoMesmoNome(string nome)
        {
            return await Db.Exames.AsNoTracking().AnyAsync(e => Equals(e.Nome.ToUpper(), nome.ToUpper()));
        }
    }
}
