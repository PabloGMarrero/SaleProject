using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data.Maping.Stores;
using SaleProject.Entities.Store;

namespace SaleProject.Data
{
    public class SystemDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public SystemDBContext(DbContextOptions<SystemDBContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMapping());
        }
    }
}
