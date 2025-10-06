using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMgmt.Web.Models;
using SchoolMgmt.Web.Services;

namespace SchoolMgmt.Web.Controllers;

public class AccountController : Controller
{
    private readonly IStudentService _svc;
    public AccountController(IStudentService svc) => _svc = svc;

    [HttpGet]
    public IActionResult Register() => View(new RegisterVm());

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var student = new Student
        {
            FirstName = vm.FirstName,
            LastName = vm.LastName,
            Age = vm.Age,
            DOB = vm.DOB,
            Gender = vm.Gender,
            Email = vm.Email,
            Phone = vm.Phone,
            Username = vm.Username,
            Qualifications = vm.Qualifications.Select(q => new Qualification {
                Course = q.Course, University = q.University, Year = q.Year, Percentage = q.Percentage
            }).ToList()
        };

        var (ok, error) = await _svc.RegisterAsync(student, vm.Password);
        if (!ok) { ModelState.AddModelError(string.Empty, error!); return View(vm); }
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _svc.AuthenticateAsync(username, password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View();
        }
        var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));
        return RedirectToAction("Index", "Students");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }
}

// ViewModels for binding
public class QualificationVm
{
    public string Course { get; set; } = string.Empty;
    public string University { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Percentage { get; set; }
}

public class RegisterVm
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public int Age { get; set; }
    public DateTime DOB { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<QualificationVm> Qualifications { get; set; } = new();
}
