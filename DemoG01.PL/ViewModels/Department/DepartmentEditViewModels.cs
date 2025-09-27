namespace DemoG01.PL.ViewModels.Department
{
    public class DepartmentEditViewModels
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }
    }
}
