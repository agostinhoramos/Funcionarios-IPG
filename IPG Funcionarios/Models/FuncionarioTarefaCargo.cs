﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class FuncionarioTarefaCargo
    {
        public int FuncionarioForeignKey { get; set; }
        public int TarefaForeignKey { get; set; }
        public int CargoForeignKey { get; set; }

        public Funcionario Funcionario { get; set; }
        public Tarefa Tarefa { get; set; }
        public Cargo Cargo { get; set; }
    }
}
