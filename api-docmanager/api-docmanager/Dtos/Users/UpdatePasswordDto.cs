using System.ComponentModel.DataAnnotations;

namespace api_docmanager.Dtos.Users;

public class UpdatePasswordDto
{
    [Required(ErrorMessage = "The {0} is required")]
    public required string CurrentPassword { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    public required string Password { get; set; }
}