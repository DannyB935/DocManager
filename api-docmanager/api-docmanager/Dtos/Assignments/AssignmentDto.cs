namespace api_docmanager.Dtos.Assignments;

public class AssignmentDto
{
    public int DocId { get; set; }
    public int UsrAssign { get; set; }
    public string? FullNameUsr { get; set; }
    public bool? Concluded { get; set; }
    public DateTime? DateConcluded { get; set; }
    public string DateConcludedStr => DateConcluded?.ToString("dd/MM/yyyy");
}