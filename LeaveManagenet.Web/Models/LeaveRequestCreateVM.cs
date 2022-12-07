using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagenet.Web.Models;

public class LeaveRequestCreateVM : IValidatableObject
{
    [Required]
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }

    [Required]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Display(Name = "Leave Types")]
    public int LeaveTypeId { get; set; }
    public SelectList? LeaveTypes { get; set; } // directamente una lista para dropdown

    [Display(Name = "Request Comments")]
    public string? RequestComments { get; set; } // si no lo pongo nullable => me lo va a 
    // hacer required

    //
    // con este, checa estas validaciones en ( ModelState.IsValid )
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(StartDate > EndDate)
        {
            yield return new ValidationResult("The end date must be grater then the start" +
                " date", new[] { nameof(StartDate), nameof(EndDate) });
        }

        if (RequestComments?.Length > 250)
        {
            yield return new ValidationResult("The comment is too long",
                new[] { nameof(RequestComments) });
        }
    }
}
