using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GCC.App.ViewModels
{
    public class ConsultaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }

        [DisplayName("Duração")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan Duracao { get; set; }

        public bool Realizada { get; set; }

        [DisplayName("Paciente")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PacienteId { get; set; }

        [DisplayName("Médico")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid MedicoId { get; set; }
        public ProntuarioViewModel Prontuario { get; set; }
        public PacienteViewModel Paciente { get; set; }
        public MedicoViewModel Medico { get; set; }
        public string Receita { get; set; }
        public string Observacao { get; set; }
        public List<Guid> ExamesId { get; set; }

        [DisplayName("Exames")]
        public string ExamesDescricao { 
            get {
                var examesNome = Exames.Select(e => e.Nome).ToArray();
                return string.Join(", ", examesNome); 
            } 
        }

        public IEnumerable<ExameViewModel> Exames { get; set; }
        public IEnumerable<PacienteViewModel> Pacientes { get; set; }
        public IEnumerable<MedicoViewModel> Medicos { get; set; }
    }
}
