using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Stores
{
    public class DetailIncomeMapping : IEntityTypeConfiguration<DetailIncome>
    {
        public void Configure(EntityTypeBuilder<DetailIncome> builder)
        {
            builder.ToTable("detailincome").HasKey(d => d.iddetailincome);
        }
    }
}
