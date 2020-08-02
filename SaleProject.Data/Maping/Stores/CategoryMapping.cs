using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Stores
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category").HasKey(c => c.idcategory);
        }
    }
}
