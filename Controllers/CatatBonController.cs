using Microsoft.AspNetCore.Mvc;

namespace catatanHutang.Controllers;

public class CatatBonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
