using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class FuncionarioViewList
    {
        public IEnumerable<Funcionario> Funcionario { get; set; }
        public PaginacaoViewModel Paginacao { get; set; }
        [DisplayName("Nome")]
        public string CurrentNome { get; set; }

    }
}
