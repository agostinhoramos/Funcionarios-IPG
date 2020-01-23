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
        public DbSet<IPG_Funcionarios.Models.Tipos_Tarefas> Tipos_Tarefas { get; set; }
        public DbSet<IPG_Funcionarios.Models.Tarefas_Professor> tarefas_Professors { get; set; }
         public DbSet<IPG_Funcionarios.Models.Feria> Feria  { get; set; }


/*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
             //Relação 1 -> N ( Cada Departamento com vários Professores )
             modelBuilder.Entity<Professor>()
                 .HasOne(mm => mm.Departamentos)
                 .WithMany(m => m.Professores)
                 .HasForeignKey(mm => mm.DepartamentoForeignKey)
                 .OnDelete(DeleteBehavior.Cascade);
             base.OnModelCreating(modelBuilder);

             //Relação 1 -> N ( Cada Professor com várias Tarefas )
             modelBuilder.Entity<Tarefa>()
                 .HasOne(mm => mm.Professores)
                 .WithMany(m => m.Tarefas)
                 .HasForeignKey(mm => mm.ProfessorForeignKey)
                 .OnDelete(DeleteBehavior.Cascade);
             base.OnModelCreating(modelBuilder);

             //Relação 1 -> N ( Cada Funcionário com várias Tarefas )
             modelBuilder.Entity<Tarefa>()
                 .HasOne(mm => mm.Funcionarios)
                 .WithMany(m => m.Tarefas)
                 .HasForeignKey(mm => mm.FuncionarioForeignKey)
                 .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
         }

     }


   */


        }
}
