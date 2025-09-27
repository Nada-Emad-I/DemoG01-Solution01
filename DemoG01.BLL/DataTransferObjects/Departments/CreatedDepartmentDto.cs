using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.DataTransferObjects.Departments
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required!!")]
        public string Name { get; set; } = null!;
        [Required]
        public string? Description { get; set; } 
        public string Code { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
    }
}
