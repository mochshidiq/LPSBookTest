
using System.ComponentModel.DataAnnotations;

namespace LPSBookTest.Models
{
    public class HistoryBuku
    {
        public int Id { get; set; }

        public int? BukuId { get; set; }

        [Required]
        [StringLength(20)]
        public string Aksi { get; set; } = string.Empty;

        [Required]
        public string Keterangan { get; set; } = string.Empty;

        public DateTime WaktuPerubahan { get; set; }
            = DateTime.Now;

        [StringLength(100)]
        public string? DiubahOleh { get; set; }
    }
}
