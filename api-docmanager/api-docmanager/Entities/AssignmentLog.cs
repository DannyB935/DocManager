using System;
using System.Collections.Generic;

namespace api_docmanager.Entities;

public partial class AssignmentLog
{
    public int Id { get; set; }

    public int DocId { get; set; }

    public int UsrAssign { get; set; }

    public DateTime? DateAssign { get; set; }

    public bool? Concluded { get; set; }

    public DateTime? DateConcluded { get; set; }

    public virtual Document Doc { get; set; } = null!;

    public virtual UserAccount UsrAssignNavigation { get; set; } = null!;
}
