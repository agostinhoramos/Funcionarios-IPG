using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IPG_Funcionarios.Models;

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
        public DbSet<IPG_Funcionarios.Models.Funcionario> Funcionario { get; set; }
        public DbSet<IPG_Funcionarios.Models.Servico> Servico { get; set; }
        public DbSet<IPG_Funcionarios.Models.Escola> Escola { get; set; }
        public DbSet<IPG_Funcionarios.Models.Cargo> Cargo { get; set; }
        public DbSet<IPG_Funcionarios.Models.Tarefa> Tarefa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Cargo>()
                .WithMany(b => b.CargoIDForeignKey)
                .WithOne();*/
        }

        public DbSet<IPG_Funcionarios.Models.Ferias> Ferias { get; set; }

      //  public DbSet<IPG_Funcionarios.Models.Ferias> Ferias { get; set; }
        

    }
}
