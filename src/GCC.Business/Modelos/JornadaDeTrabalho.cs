using GCC.Business.Modelos.Abstratos;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GCC.Business.Modelos
{
    public class JornadaDeTrabalho : Entidade
    {
        [DisplayName("Sunday")]
        public bool Domingo { get; set; }
        [DisplayName("Monday")]
        public bool Segunda { get; set; }
        [DisplayName("Tuesday")]
        public bool Terca { get; set; }
        [DisplayName("Wednesday")]
        public bool Quarta { get; set; }
        [DisplayName("Thursday")]
        public bool Quinta { get; set; }
        [DisplayName("Friday")]
        public bool Sexta { get; set; }
        [DisplayName("Saturday")]
        public bool Sabado { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public DateTime HoraInicioIntervalo { get; set; }
        public DateTime HoraFimIntervalo { get; set; }
    }
}
