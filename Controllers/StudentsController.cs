using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMgmt.Web.Services;

namespace SchoolMgmt.Web.Controllers;

[Authorize]
public class StudentsController : Controller
{
    private readonly IStudentService _svc;
    public StudentsController(IStudentService svc) => _svc = svc;

    public async Task<IActionResult> Index()
    {
        var list = await _svc.ListAsync();
        return View(list);
    }
}
