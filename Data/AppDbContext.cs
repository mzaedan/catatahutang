using catatanHutang.Models.Barang;
using catatanHutang.Models.Pelanggan;
using Microsoft.EntityFrameworkCore;

namespace catatanHutang.Data;

/// <summary>
/// DbContext utama aplikasi catatanHutang.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Barang> Barangs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map entity User ke tabel "user" (MySQL reserved keyword, akan di-escape otomatis oleh Pomelo)
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();
        });

        // Map entity Barang ke tabel "barang"
        modelBuilder.Entity<Barang>(entity =>
        {
            entity.ToTable("barang");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).ValueGeneratedOnAdd();
        });
    }
}