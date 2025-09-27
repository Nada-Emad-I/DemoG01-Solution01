using DemoG01.DAL.Models.DepartmentModels;
using DemoG01.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Models.EmployeeModels
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Gender Gender { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public string? ImageName { get; set; }
    }
}
