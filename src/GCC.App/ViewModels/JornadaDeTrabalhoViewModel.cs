using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace GCC.App.ViewModels
{
    public class JornadaDeTrabalhoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public bool Domingo { get; set; }
        public bool Segunda { get; set; }

        [DisplayName("Terça")]
        public bool Terca { get; set; }
        public bool Quarta { get; set; }
        public bool Quinta { get; set; }
        public bool Sexta { get; set; }

        [DisplayName("Sábado")]
        public bool Sabado { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Hora início")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Hora fim")]
        public DateTime HoraFim { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Hora início intervalo")]
        public DateTime HoraInicioIntervalo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Hora fim intervalo")]
        public DateTime HoraFimIntervalo { get; set; }

        [HiddenInput]
        public Guid MedicoId { get; set; }
    }
}
