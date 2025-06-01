using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadEvalSys.Domain.Entities;

public class Coordinator
{
    public string UserId { get; set; }
    public string TechnicalCareerId { get; set; }

    public virtual User User { get; set; }
    public virtual TechnicalCareer TechnicalCareer { get; set; }
}