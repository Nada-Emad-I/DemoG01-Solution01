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
    internal class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T:BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            //throw new NotImplementedException();
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}
