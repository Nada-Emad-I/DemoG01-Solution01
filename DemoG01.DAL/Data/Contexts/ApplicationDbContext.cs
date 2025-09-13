using DemoG01.DAL.Data.Configurations;
using DemoG01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Data.Contexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
             
        }
        public DbSet<Department> Departments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
