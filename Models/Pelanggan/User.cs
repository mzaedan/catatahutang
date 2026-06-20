using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace catatanHutang.Models.Pelanggan;

/// <summary>
/// Entitas User yang merepresentasikan data pelanggan.
/// Dipetakan ke tabel "user" di database MySQL.
/// </summary>
[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nama_pelanggan")]
    [Display(Name = "Nama Pelanggan")]
    public string NamaPelanggan { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("no_hp")]
    [Display(Name = "No. HP")]
    public string NoHP { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    [Column("alamat")]
    [Display(Name = "Alamat")]
    public string Alamat { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
