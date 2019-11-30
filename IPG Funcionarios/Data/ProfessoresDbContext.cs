using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPG_Funcionarios.Data
{
    public class ProfessoresDbContext : DbContext
    {
        public ProfessoresDbContext(DbContextOptions<ProfessoresDbContext> options) : base(options) {}

        public DbSet<IPG_Funcionarios.Models.Professor> Professor { get; set; }
    }
}
