﻿using GCC.Business.Modelos.Abstratos;
using System;
using System.Collections.Generic;

namespace GCC.Business.Modelos
{
    public class Medico : PessoaAbstrata
    {
        public CRM CRM { get; set; }
        public Especialidade Especialidade { get; set; }
        public List<DiaDeTrabalho> Disponibilidade { get; set; }
    }
}
