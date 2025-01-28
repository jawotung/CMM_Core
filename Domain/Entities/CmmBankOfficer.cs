using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmBankOfficer
{
    public Guid OfficerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleInitial { get; set; }

    public int? OfficerTypeId { get; set; }

    public string? IsActive { get; set; }

    public DateOnly? DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? DeactivationDate { get; set; }

    public string? DeactivateBy { get; set; }
}
