using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UniControl.Models
{
    public partial class AplicacaoDbContext : DbContext
    {
        public AplicacaoDbContext()
        {
        }

        public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contato> Contato { get; set; }

    }
}
