using System.ComponentModel.DataAnnotations;

namespace catatanHutang.Models.Barang;

public class BarangViewModel
{
    [Required(ErrorMessage = "Nama barang wajib diisi.")]
    [StringLength(150, ErrorMessage = "Nama barang maksimal 150 karakter.")]
    [Display(Name = "Nama Barang")]
    public string NamaBarang { get; set; } = string.Empty;

    [Required(ErrorMessage = "Harga jual wajib diisi.")]
    [Range(0, double.MaxValue, ErrorMessage = "Harga jual harus berupa angka positif.")]
    [Display(Name = "Harga Jual")]
    public decimal? HargaJual { get; set; }

    [Required(ErrorMessage = "Satuan wajib diisi.")]
    [StringLength(50, ErrorMessage = "Satuan maksimal 50 karakter.")]
    [Display(Name = "Satuan")]
    public string Satuan { get; set; } = string.Empty;
}
