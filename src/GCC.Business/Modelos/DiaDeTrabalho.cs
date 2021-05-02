using GCC.Business.Modelos.Abstratos;
using System;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class DiaDeTrabalho : Entidade
    {
        public DateTime Dia { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public DateTime HoraInicioIntervalo { get; set; }
        public DateTime HoraFimIntervalo { get; set; }
        public List<Consulta> Consultas { get; set; } 
    }
}
