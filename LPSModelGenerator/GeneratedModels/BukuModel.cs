#nullable enable
using System;

namespace Models
{
    public class BukuModel
    {
        public int Id { get; set; }
        public string Judul { get; set; } = string.Empty;
        public string Penulis { get; set; } = string.Empty;
        public int JenisBukuId { get; set; }
        public DateTime TanggalRilis { get; set; }
        public int JumlahHalaman { get; set; }
    }
}
