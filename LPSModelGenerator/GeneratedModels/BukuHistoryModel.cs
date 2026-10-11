#nullable enable
using System;

namespace Models
{
    public class BukuHistoryModel
    {
        public int Id { get; set; }
        public int? BukuId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? OldData { get; set; }
        public string? NewData { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
