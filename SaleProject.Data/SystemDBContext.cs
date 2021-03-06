﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SaleProject.Data.Maping.Sales;
using SaleProject.Data.Maping.Stores;
using SaleProject.Data.Maping.Users;
using SaleProject.Entities.Sales;
using SaleProject.Entities.Store;
using SaleProject.Entities.Users;

namespace SaleProject.Data
{
    public class SystemDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<DetailIncome> DetailsIncome { get; set; }

        public SystemDBContext(DbContextOptions<SystemDBContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ItemMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new IncomeMapping());
            modelBuilder.ApplyConfiguration(new DetailIncomeMapping());
        }
    }
}
