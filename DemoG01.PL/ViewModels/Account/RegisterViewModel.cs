using System.ComponentModel.DataAnnotations;

namespace DemoG01.PL.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="FirstName is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
