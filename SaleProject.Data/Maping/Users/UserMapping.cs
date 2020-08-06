using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaleProject.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleProject.Data.Maping.Users
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("userperson").HasKey(u => u.iduserperson);
        }
    }
}
