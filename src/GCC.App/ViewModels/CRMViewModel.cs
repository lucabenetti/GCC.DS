using GCC.Business.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GCC.App.ViewModels
{
    public class CRMViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public UFEnum UF { get; set; }
    }
}