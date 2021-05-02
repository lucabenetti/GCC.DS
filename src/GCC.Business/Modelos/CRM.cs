using GCC.Business.Modelos.Abstratos;
using GCC.Business.Enums;
using System;

namespace GCC.Business.Modelos
{
    public class CRM : Entidade
    {
        public int Numero { get; set; }
        public UFEnum UF { get; set; }
    }
}