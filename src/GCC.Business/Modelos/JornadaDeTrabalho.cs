using GCC.Business.Modelos.Abstratos;
using System;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class JornadaDeTrabalho : Entidade
    {
        public bool Domingo { get; set; }
        public bool Segunda { get; set; }
        public bool Terca { get; set; }
        public bool Quarta { get; set; }
        public bool Quinta { get; set; }
        public bool Sexta { get; set; }
        public bool Sabado { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public DateTime HoraInicioIntervalo { get; set; }
        public DateTime HoraFimIntervalo { get; set; }
    }
}
