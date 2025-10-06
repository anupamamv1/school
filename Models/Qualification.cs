using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMgmt.Web.Models;

public class Qualification
{
    public int Id { get; set; }
    [Required] public string Course { get; set; } = string.Empty;
    [Required] public string University { get; set; } = string.Empty;
    [Range(1900, 2100)] public int Year { get; set; }
    [Range(0, 100)] public decimal Percentage { get; set; }
    
    public int StudentId { get; set; }
    [ForeignKey(nameof(StudentId))]
    public Student? Student { get; set; }
}
