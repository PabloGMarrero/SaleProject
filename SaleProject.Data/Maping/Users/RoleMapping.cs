using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Users
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("rol").HasKey(r => r.idrol);
        }
    }
}
