using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.DataTransferObjects.Departments
{
    public class DepartmentDetailsDto
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }=string.Empty;
        public string Code { get; set; }=null!;
        public int CreatedBy { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public int LastModifiedBy { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
