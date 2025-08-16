using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api_docmanager.Dtos.Assignments;

public class CreateAssignDto
{
    [DisplayName("Document Id")]
    [Required(ErrorMessage = "The {0} is required")]
    public int DocId { get; set; }

    [DisplayName("User Id")]
    [Required(ErrorMessage = "The {0} is required")]
    public int UsrAssign { get; set; }
}