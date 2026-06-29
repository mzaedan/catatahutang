using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using catatanHutang.Data;
using catatanHutang.Models.Barang;

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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var barang = await _dbContext.Barangs
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);

        if (barang is null)
        {
            return NotFound();
        }

        var model = new BarangViewModel
        {
            Id = barang.Id,
            NamaBarang = barang.NamaBarang,
            HargaJual = barang.HargaJual,
            Satuan = barang.Satuan
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BarangViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var barang = await _dbContext.Barangs.FindAsync(id);
        if (barang is null)
        {
            return NotFound();
        }

        barang.NamaBarang = model.NamaBarang.Trim();
        barang.HargaJual = model.HargaJual ?? 0;
        barang.Satuan = model.Satuan.Trim();

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Gagal memperbarui data barang.");
            ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat memperbarui data. Silakan coba lagi.");
            return View(model);
        }

        TempData["SuccessMessage"] = $"Barang \"{model.NamaBarang}\" berhasil diperbarui.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var barang = await _dbContext.Barangs.FindAsync(id);
        if (barang is null)
        {
            TempData["ErrorMessage"] = "Barang tidak ditemukan.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _dbContext.Barangs.Remove(barang);
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Gagal menghapus data barang.");
            TempData["ErrorMessage"] = "Terjadi kesalahan saat menghapus data. Silakan coba lagi.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"Barang \"{barang.NamaBarang}\" berhasil dihapus.";

        return RedirectToAction(nameof(Index));
    }
}
