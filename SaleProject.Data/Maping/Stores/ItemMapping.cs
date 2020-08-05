using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Stores
{
    class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("item").HasKey(c => c.iditem);
        }
    }
}
