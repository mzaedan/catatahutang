using Microsoft.AspNetCore.Mvc;

namespace catatanHutang.Controllers;

public class BarangController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
