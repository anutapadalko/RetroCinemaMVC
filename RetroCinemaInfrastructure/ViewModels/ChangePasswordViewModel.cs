using System.ComponentModel.DataAnnotations;

namespace RetroCinema.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        [MinLength(8, ErrorMessage = "Пароль має бути не менше 8 символів")]
        public string NewPassword { get; set; }
    }
}