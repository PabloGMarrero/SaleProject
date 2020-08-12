using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Stores
{
    public class IncomeMapping : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("income").HasKey(i => i.idincome);
            builder.HasOne(p => p.person)
                .WithMany(i => i.incomes)
                .HasForeignKey(i => i.idsupplier);
        }
    }
}
