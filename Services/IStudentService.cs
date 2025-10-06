using SchoolMgmt.Web.Models;

namespace SchoolMgmt.Web.Services;

public interface IStudentService
{
    Task<(bool Ok, string? Error)> RegisterAsync(Student student, string passwordPlain);
    Task<Student?> AuthenticateAsync(string username, string passwordPlain);
    Task<List<Student>> ListAsync();
}
