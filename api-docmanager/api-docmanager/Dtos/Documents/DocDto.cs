namespace api_docmanager.Dtos.Documents;

public class DocDto
{
    public int Id { get; set; }
    public string? DocNum { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    
    public int? UsrSender { get; set; }
    
    public string? NameSender { get; set; }
    public string? LnameSender { get; set; }
    public string? TitleSender { get; set; }
    public string? PositionSender { get; set; }
    public string? FullNameSender { get; set; }

    public int? UsrRecip { get; set; }
    public string? NameRecip { get; set; }
    public string? LnameRecip { get; set; }
    public string? TitleRecip { get; set; }
    public string? PositionRecip { get; set; }
    public string? FullNameRecip { get; set; }
    
    public int? UnitBelong { get; set; }
    public string? UnitBelongName { get; set; }
    
    public string? DeptName { get; set; }
    
    public DateTime? DateCreate { get; set; }
    public string? DateCreateStr => DateCreate?.ToString("dd/MM/yyyy");
    public DateTime? DateDone { get; set; }
    public string? DateDoneStr => DateDone?.ToString("dd/MM/yyyy");
    
    public int? GenByUsr { get; set; }
    public string? GenByUsrName { get; set; }
    
    public int? DocType { get; set; }
    
    public bool? Anonym { get; set; }
}