using DemoG01.DAL.Models.EmployeeModels;
using DemoG01.DAL.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Data.Configurations
{
    internal class EmployeeCofiguration :BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            //throw new NotImplementedException();
            builder.Property(E => E.Address).HasColumnType("varchar(50)");
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            builder.Property(E=>E.Gender).HasConversion((empGender) => empGender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender),gender));
            builder.Property(E=>E.EmployeeType).HasConversion((empType) => empType.ToString(),
                (employeeType) =>(EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType));
            base.Configure(builder);
        }
    }
}
