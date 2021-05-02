using GCC.Business.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCC.Data.Mapeamento
{
    public class CRMMapeamento : IEntityTypeConfiguration<CRM>
    {
        public void Configure(EntityTypeBuilder<CRM> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Numero).IsRequired();
            builder.Property(c => c.UF).IsRequired();
            builder.ToTable("CRM");
        }
    }
}
