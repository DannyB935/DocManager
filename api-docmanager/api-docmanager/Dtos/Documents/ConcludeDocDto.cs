using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api_docmanager.Dtos.Documents;

public class ConcludeDocDto
{
    [DisplayName("Document Id")]
    [Required(ErrorMessage = "The {0} is required")]
    public int DocId { get; set; }

    [DisplayName("User Id")]
    [Required(ErrorMessage = "The {0} is required")]
    public int UsrAssign { get; set; }

    public bool? Concluded = true;
    
    public DateTime? DateConcluded = DateTime.Now;
}