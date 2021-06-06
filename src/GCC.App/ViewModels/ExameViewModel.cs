using System;
using System.ComponentModel.DataAnnotations;

namespace GCC.App.ViewModels
{
    public class ExameViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
    }
}
