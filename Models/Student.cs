using System.ComponentModel.DataAnnotations;

namespace SchoolMgmt.Web.Models;

public class Student
{
    public int Id { get; set; }
    
    [Required]
    public string StudentCode { get; set; } = string.Empty; // e.g. STU-0001
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    
    [Required, Range(1,150)]
    public int Age { get; set; }
    
    [Required, DataType(DataType.Date)]
    public DateTime DOB { get; set; }
    
    [Required]
    public string Gender { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required, Phone]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public List<Qualification> Qualifications { get; set; } = new();
}
