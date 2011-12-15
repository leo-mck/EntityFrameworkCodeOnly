using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TesteEF
{
    public class CarroDados : DbContext
    {
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Marca> Marcas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Carro>().ToTable("Carro");

            base.OnModelCreating(modelBuilder);
        }
    }
}

