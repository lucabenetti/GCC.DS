using System;
using GCC.Business.Enums;
using GCC.Business.Modelos.Identity;
using Microsoft.AspNetCore.Identity;

namespace GCC.Business.Modelos.Abstratos
{
    public abstract class PessoaAbstrata : Entidade
    {
        public Usuario Usuario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public SexoEnum Sexo { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
