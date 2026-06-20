using catatanHutang.Data;
using catatanHutang.Models.Pelanggan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catatanHutang.Controllers;

public class PelangganController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<PelangganController> _logger;

    public PelangganController(AppDbContext dbContext, ILogger<PelangganController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var pelangganList = await _dbContext.Users
            .AsNoTracking()
            .OrderByDescending(u => u.Id)
            .ToListAsync();

        return View(pelangganList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new PelangganViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PelangganViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new User
        {
            NamaPelanggan = model.NamaPelanggan.Trim(),
            NoHP = model.NoHP.Trim(),
            Alamat = model.Alamat.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Gagal menyimpan data pelanggan ke database.");
            ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat menyimpan data. Silakan coba lagi.");
            return View(model);
        }

        TempData["SuccessMessage"] = $"Pelanggan \"{model.NamaPelanggan}\" berhasil ditambahkan.";

        return RedirectToAction(nameof(Index));
    }
}
