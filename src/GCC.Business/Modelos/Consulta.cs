using GCC.Business.Modelos.Abstratos;
using System;

namespace GCC.Business.Modelos
{
    public class Consulta : Entidade
    {
        public DateTime Data { get; set; }
        public TimeSpan Duracao { get; set; }
        public Prontuario Prontuario { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public bool Realizada { get; set; }
    }
}
