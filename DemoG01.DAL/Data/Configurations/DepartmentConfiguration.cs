using DemoG01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //throw new NotImplementedException();
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("Varchar(20)");
            builder.Property(D => D.Code).HasColumnType("Varchar(20)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");

        }
    }
}
