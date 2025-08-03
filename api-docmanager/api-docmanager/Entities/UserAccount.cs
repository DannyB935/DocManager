using System;
using System.Collections.Generic;

namespace api_docmanager.Entities;

public partial class UserAccount
{
    public int Code { get; set; }

    public string NameUsr { get; set; } = null!;

    public string? Lname { get; set; }

    public string? Email { get; set; }

    public int? UnitBelong { get; set; }

    public int? UsrRole { get; set; }

    public bool? Deleted { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<AssignmentLog> AssignmentLogs { get; set; } = new List<AssignmentLog>();

    public virtual ICollection<Document> DocumentGenByUsrNavigations { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentUsrAssignNavigations { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentUsrRecipNavigations { get; set; } = new List<Document>();

    public virtual ICollection<Document> DocumentUsrSenderNavigations { get; set; } = new List<Document>();

    public virtual Unit? UnitBelongNavigation { get; set; }

    public virtual UserRole? UsrRoleNavigation { get; set; }
}
