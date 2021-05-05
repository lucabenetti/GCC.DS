using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GCC.App.ViewModels
{
    public class ConsultaViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Duracao { get; set; }
        public ProntuarioViewModel Prontuario { get; set; }
        public PacienteViewModel Paciente { get; set; }
        public MedicoViewModel Medico { get; set; }
        public bool Realizada { get; set; }
    }
}
