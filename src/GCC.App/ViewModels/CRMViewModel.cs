using GCC.Business.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GCC.App.ViewModels
{
    public class CRMViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CRM Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CRM UF")]
        public UFEnum UF { get; set; }
    }
}