using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolMgmt.Web.Data;
using SchoolMgmt.Web.Models;

namespace SchoolMgmt.Web.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _db;
    private readonly PasswordHasher<Student> _hasher = new();

    public StudentService(AppDbContext db) => _db = db;

    public async Task<(bool Ok, string? Error)> RegisterAsync(Student s, string passwordPlain)
    {
        if (await _db.Students.AnyAsync(x => x.Username == s.Username)) return (false, "Username already exists");
        if (await _db.Students.AnyAsync(x => x.Email == s.Email)) return (false, "Email already exists");

        // Generate simple StudentCode like STU-0001
        var next = (await _db.Students.CountAsync()) + 1;
        s.StudentCode = $"STU-{next.ToString("D4")}";

        s.PasswordHash = _hasher.HashPassword(s, passwordPlain);
        _db.Students.Add(s);
        await _db.SaveChangesAsync();
        return (true, null);
    }

    public async Task<Student?> AuthenticateAsync(string username, string passwordPlain)
    {
        var user = await _db.Students.FirstOrDefaultAsync(x => x.Username == username);
        if (user is null) return null;
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, passwordPlain);
        return result == PasswordVerificationResult.Failed ? null : user;
    }

    public Task<List<Student>> ListAsync() =>
        _db.Students.Include(s => s.Qualifications).OrderByDescending(s => s.Id).ToListAsync();
}
