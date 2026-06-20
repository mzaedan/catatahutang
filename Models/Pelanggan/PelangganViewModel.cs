using System.ComponentModel.DataAnnotations;

namespace catatanHutang.Models.Pelanggan;

public class PelangganViewModel
{
    [Required(ErrorMessage = "Nama pelanggan wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama pelanggan maksimal 100 karakter.")]
    [Display(Name = "Nama Pelanggan")]
    public string NamaPelanggan { get; set; } = string.Empty;

    [Required(ErrorMessage = "No. HP wajib diisi.")]
    [StringLength(20, ErrorMessage = "No. HP maksimal 20 karakter.")]
    [Display(Name = "No. HP")]
    [RegularExpression(@"^[0-9+\-\s()]+$", ErrorMessage = "No. HP hanya boleh berisi angka, spasi, tanda plus, strip, atau kurung.")]
    public string NoHP { get; set; } = string.Empty;

    [Required(ErrorMessage = "Alamat wajib diisi.")]
    [StringLength(250, ErrorMessage = "Alamat maksimal 250 karakter.")]
    [Display(Name = "Alamat")]
    [DataType(DataType.MultilineText)]
    public string Alamat { get; set; } = string.Empty;
}
