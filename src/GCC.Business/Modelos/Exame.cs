using GCC.Business.Modelos.Abstratos;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class Exame : Entidade
    {
        public string Nome { get; set; }

        public List<Consulta> Consulta { get; set; }
    }
}
