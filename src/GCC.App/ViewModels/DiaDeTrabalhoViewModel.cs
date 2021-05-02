using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GCC.App.ViewModels
{
    public class DiaDeTrabalhoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Dia { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
        public DateTime HoraInicioIntervalo { get; set; }
        public DateTime HoraFimIntervalo { get; set; }
        public List<ConsultaViewModel> Consultas { get; set; }
    }
}