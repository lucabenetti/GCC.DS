using GCC.Business.Modelos.Abstratos;
using System;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class Consulta : Entidade
    {
        public DateTime Data { get; set; }
        public TimeSpan Duracao { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public List<Exame> Exame { get; set; }
        public string Receita { get; set; }
        public string Observacao { get; set; }
        public bool Realizada { get; set; }
        public Guid PacienteId { get; set; }
        public Guid MedicoId { get; set; }
    }
}
