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
        public DbSet<IPG_Funcionarios.Models.Ferias> Ferias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relação 1 -> N ( Cada Departamento com vários Professores )
            modelBuilder.Entity<Professor>()
                .HasOne(mm => mm.Departamento)
                .WithMany(m => m.Professores)
                .HasForeignKey(mm => mm.DepartamentoForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Escola com vários Departamentos )
            modelBuilder.Entity<Departamento>()
                .HasOne(mm => mm.Escola)
                .WithMany(m => m.Departamentos)
                .HasForeignKey(mm => mm.EscolaForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Escola com vários Serviços )
            modelBuilder.Entity<Servico>()
                .HasOne(mm => mm.Escola)
                .WithMany(m => m.Servicos)
                .HasForeignKey(mm => mm.EscolaForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Funcionário com vários Serviços )
            modelBuilder.Entity<Servico>()
                .HasOne(mm => mm.Funcionario)
                .WithMany(m => m.Servicos)
                .HasForeignKey(mm => mm.FuncionarioForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Funcionário com várias Férias )
            modelBuilder.Entity<Ferias>()
                .HasOne(mm => mm.Funcionario)
                .WithMany(m => m.Ferias)
                .HasForeignKey(mm => mm.FuncionarioForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Professor com várias Férias )
            modelBuilder.Entity<Ferias>()
                .HasOne(mm => mm.Professor)
                .WithMany(m => m.Ferias)
                .HasForeignKey(mm => mm.ProfessorForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Cargo com várias Cargos )
            modelBuilder.Entity<Cargo>()
                .HasOne(mm => mm.Chefe)
                .WithMany()
                .HasForeignKey(m => m.CargoChefe);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Funcionário com vários FuncionarioTarefaCargo )
            modelBuilder.Entity<FuncionarioTarefaCargo>()
                .HasOne(mm => mm.Funcionario)
                .WithMany(m => m.FuncionarioTarefaCargos)
                .HasForeignKey(mm => mm.FuncionarioForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Tarefa com vários FuncionarioTarefaCargo )
            modelBuilder.Entity<FuncionarioTarefaCargo>()
                .HasOne(mm => mm.Tarefa)
                .WithMany(m => m.FuncionarioTarefaCargos)
                .HasForeignKey(mm => mm.TarefaForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Cargo com vários FuncionarioTarefaCargo )
            modelBuilder.Entity<FuncionarioTarefaCargo>()
                .HasOne(mm => mm.Cargo)
                .WithMany(m => m.FuncionarioTarefaCargos)
                .HasForeignKey(mm => mm.CargoForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Professor com vários ProfessorTarefaCargo )
            modelBuilder.Entity<ProfessorTarefaCargo>()
                .HasOne(mm => mm.Professor)
                .WithMany(m => m.ProfessorTarefaCargos)
                .HasForeignKey(mm => mm.ProfessorForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Tarefa com vários ProfessorTarefaCargo )
            modelBuilder.Entity<ProfessorTarefaCargo>()
                .HasOne(mm => mm.Tarefa)
                .WithMany(m => m.ProfessorTarefaCargos)
                .HasForeignKey(mm => mm.TarefaForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            //Relação 1 -> N ( Cada Cargo com vários ProfessorTarefaCargo )
            modelBuilder.Entity<ProfessorTarefaCargo>()
                .HasOne(mm => mm.Cargo)
                .WithMany(m => m.ProfessorTarefaCargos)
                .HasForeignKey(mm => mm.CargoForeignKey)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}