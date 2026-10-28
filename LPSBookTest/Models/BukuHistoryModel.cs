using System.ComponentModel.DataAnnotations;
using System;

namespace LPSBookTest.Models
{
    public class BukuHistoryModel
    {
        public int Id { get; set; }

        public int? BukuId { get; set; }
        [Required]
        [StringLength(20)]
        public string Action { get; set; } = string.Empty;

        public string? OldData { get; set; }

        public string? NewData { get; set; }

        public DateTime ChangedAt { get; set; }

        public BukuModel? Buku { get; set; }
    }
}
