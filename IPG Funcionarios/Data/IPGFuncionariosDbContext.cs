using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPG_Funcionarios.Models
{
    public class IPGFuncionariosDbContext : DbContext
    {
        public IPGFuncionariosDbContext(DbContextOptions<IPGFuncionariosDbContext> options)
            : base(options)
        {
        }

        public DbSet<IPG_Funcionarios.Models.Professor> Professor { get; set; }
    }
}
