using catatanHutang.Data;
using catatanHutang.Models.Barang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace catatanHutang.Controllers;

public class BarangController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<BarangController> _logger;

    public BarangController(AppDbContext dbContext, ILogger<BarangController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var barangList = await _dbContext.Barangs
            .AsNoTracking()
            .OrderByDescending(b => b.Id)
            .ToListAsync();

        return View(barangList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new BarangViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BarangViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var barang = new Barang
        {
            NamaBarang = model.NamaBarang.Trim(),
            HargaJual = model.HargaJual ?? 0,
            Satuan = model.Satuan.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            _dbContext.Barangs.Add(barang);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Gagal menyimpan data barang ke database.");
            ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat menyimpan data. Silakan coba lagi.");
            return View(model);
        }

        TempData["SuccessMessage"] = $"Barang \"{model.NamaBarang}\" berhasil ditambahkan.";

        return RedirectToAction(nameof(Index));
    }
}
