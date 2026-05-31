using Microsoft.AspNetCore.Mvc;

namespace catatanHutang.Controllers;

public class PelangganController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
