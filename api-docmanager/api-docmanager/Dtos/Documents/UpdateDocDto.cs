using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api_docmanager.Dtos.Documents;

public class UpdateDocDto
{
    
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(maximumLength: 128, MinimumLength = 5, ErrorMessage = "The {0} must be between {2} and {1} characters.")]
    public string? Subject { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [StringLength(maximumLength: 1042, MinimumLength = 5, ErrorMessage = "The {0} must be between {2} and {1} characters.")]
    public string? Body { get; set; }
    
    public int? UsrSender { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(64, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? NameSender { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(128, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? LnameSender { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(64, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? TitleSender { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(128, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? PositionSender { get; set; }

    public int? UsrRecip { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(64, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? NameRecip { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(128, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? LnameRecip { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(64, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? TitleRecip { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [MaxLength(128, ErrorMessage = "The field cannot be longer than {1} characters.")]
    public string? PositionRecip { get; set; }

    [Display(Name = "Unit of Work")]
    [Required(ErrorMessage = "The {0} is required")]
    public int? UnitBelong { get; set; }
    
    [DisplayName("Department Name")]
    [StringLength(256, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
    public string? DeptName { get; set; }
    
    [Required(ErrorMessage = "The {0} is required")]
    [Range(0, 1, ErrorMessage = "The {0} must be either {1} or {2}.")]
    public int? DocType { get; set; }
    
    public bool? Anonym { get; set; }
}