using System;

namespace GCC.Business.Modelos.Abstratos
{
    public abstract class Entidade
    {
        protected Entidade()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
