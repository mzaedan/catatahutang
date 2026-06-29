using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace catatanHutang.Models.Barang;

/// <summary>
/// Entitas Barang yang merepresentasikan data barang.
/// Dipetakan ke tabel "barang" di database MySQL.
/// </summary>
[Table("barang")]
public class Barang
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("nama_barang")]
    [Display(Name = "Nama Barang")]
    public string NamaBarang { get; set; } = string.Empty;

    [Column("harga_jual", TypeName = "decimal(18,2)")]
    [Display(Name = "Harga Jual")]
    public decimal HargaJual { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("satuan")]
    [Display(Name = "Satuan")]
    public string Satuan { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
