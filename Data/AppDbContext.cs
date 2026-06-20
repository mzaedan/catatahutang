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
    }
}
