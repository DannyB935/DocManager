using System;
using System.Collections.Generic;

namespace api_docmanager.Entities;

public partial class Document
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

    public int? UsrRecip { get; set; }

    public string? NameRecip { get; set; }

    public string? LnameRecip { get; set; }

    public string? TitleRecip { get; set; }

    public string? PositionRecip { get; set; }

    public int? UnitBelong { get; set; }

    public string? DeptName { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateDone { get; set; }

    public int? UsrAssign { get; set; }

    public int? GenByUsr { get; set; }

    public int? DocType { get; set; }

    public bool? Anonym { get; set; }

    public bool? Deleted { get; set; }

    public bool? Concluded { get; set; }

    /// <summary>
    /// If the document is registered or created. 0 = created, 1 = registered
    /// </summary>
    public byte? Registered { get; set; }

    public virtual ICollection<AssignmentLog> AssignmentLogs { get; set; } = new List<AssignmentLog>();

    public virtual UserAccount? GenByUsrNavigation { get; set; }

    public virtual Unit? UnitBelongNavigation { get; set; }

    public virtual UserAccount? UsrAssignNavigation { get; set; }

    public virtual UserAccount? UsrRecipNavigation { get; set; }

    public virtual UserAccount? UsrSenderNavigation { get; set; }
}
