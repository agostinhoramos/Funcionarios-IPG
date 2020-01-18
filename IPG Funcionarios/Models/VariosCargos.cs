using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG_Funcionarios.Models
{
    public class VariosCargos
    {
        public int CargoForeignKey { get; set; }
        public Cargo Cargo { get; set; }
    }
}
