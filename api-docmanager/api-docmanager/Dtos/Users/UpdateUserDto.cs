using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace api_docmanager.Dtos.Users;

public class UpdateUserDto
{
    [FromForm(Name = "full_name")]
    [Required(ErrorMessage="The full name is required")]
    [StringLength(maximumLength: 64, MinimumLength = 2, ErrorMessage = "The name must be between {2} and {1} characters")]
    public required string NameUsr { get; set; }
    
    [FromForm(Name = "last_name")]
    [Required(ErrorMessage = "The last name is required")]
    [StringLength(maximumLength: 128, MinimumLength = 3, ErrorMessage = "The last name must be between {2} and {1} characters")]
    public required string LName { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(maximumLength: 256, MinimumLength = 8, ErrorMessage = "The password must be between {2} and {1} characters")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }
    
    [FromForm(Name = "unit")]
    [Required(ErrorMessage = "The {0} has to be chosen")]
    [Range(1, int.MaxValue, ErrorMessage = "The {0} value is not valid")]
    public required int UnitBelong { get; set; }
    
}