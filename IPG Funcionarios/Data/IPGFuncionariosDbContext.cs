using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IPG_Funcionarios.Models;

namespace IPG_Funcionarios.Data {
    public class IPGFuncionariosDbContext : IdentityDbContext {
        public IPGFuncionariosDbContext(DbContextOptions<IPGFuncionariosDbContext> options)
            : base(options) {
        }
        public DbSet<IPG_Funcionarios.Models.Departamento> Departamento { get; set; }
    }
}
