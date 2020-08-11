using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Sales
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person").HasKey(p => p.idperson);
        }
    }
}
