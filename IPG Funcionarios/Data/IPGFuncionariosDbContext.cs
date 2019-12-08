using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IPG_Funcionarios.Models;
using IPG_Funcionarios.Data;

namespace IPG_Funcionarios.Models
{
    public class IPGFuncionariosDbContext : DbContext
    {
        public IPGFuncionariosDbContext(DbContextOptions<IPGFuncionariosDbContext> options)
            : base(options)
        {
        }
        public DbSet<IPG_Funcionarios.Models.Professor> Professor { get; set; }
        public DbSet<IPG_Funcionarios.Models.Departamento> Departamento { get; set; }
        public object Funcionarios { get; internal set; }

       
    }
}
