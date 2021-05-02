using GCC.Business.Modelos.Abstratos;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class Paciente : PessoaAbstrata
    {
        public string NomeDaMae { get; set; }
        public List<Consulta> Consultas { get; set; }
    }
}
