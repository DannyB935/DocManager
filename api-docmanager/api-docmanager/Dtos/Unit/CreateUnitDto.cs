using System.ComponentModel.DataAnnotations;

namespace api_docmanager.Dtos.Unit;

public class CreateUnitDto
{
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(64, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
    public required string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(maximumLength: 32, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} characters.")]
    public required string Prefix { get; set; }
    
    //Model validation
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Prefix))
        {
            string upperPrefix = Prefix.ToUpper();
            if (Prefix != upperPrefix)
            {
                yield return new ValidationResult("The prefix must be in upper case format.", new string[] { nameof(Prefix) });
            }
        }
    }
}