using LPSBookTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LPSBookTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Models.BukuModel> Bukus { get; set; } = null!;
        public DbSet<Models.JenisBukuModel> JenisBukus { get; set; } = null!;
        public DbSet<Models.BukuHistoryModel> BukuHistories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JenisBukuModel>(entity =>
            {
                entity.ToTable("JenisBuku");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.JenisBuku)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired();
            });

            modelBuilder.Entity<BukuModel>(entity =>
            {
                entity.ToTable("Buku");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Judul)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(x => x.Penulis)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(x => x.TanggalRilis)
                    .IsRequired();

                entity.Property(x => x.JumlahHalaman)
                    .IsRequired();

                entity.HasOne(x => x.JenisBuku)
                    .WithMany(x => x.Bukus)
                    .HasForeignKey(x => x.JenisBukuId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BukuHistoryModel>(entity =>
            {
                entity.ToTable("BukuHistory");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Action)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(x => x.ChangedAt)
                    .IsRequired();  

                entity.HasOne(x => x.Buku)
                    .WithMany()
                    .HasForeignKey(x => x.BukuId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }        
}
