using System.ComponentModel.DataAnnotations;

namespace LPSBookTest.Models
{
    public class BukuModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Judul wajib diisi.")]
        [StringLength(100, ErrorMessage = "Judul maksimal 100 karakter.")]
        public string Judul { get; set; } = string.Empty;

        [Required(ErrorMessage = "Penulis wajib diisi.")]
        [StringLength(100, ErrorMessage = "Penulis maksimal 100 karakter.")]
        public string Penulis { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jenis Buku wajib dipilih.")]
        public int JenisBukuId { get; set; }

        [Required(ErrorMessage = "Tanggal Rilis wajib diisi.")]
        [DataType(DataType.Date)]
        public DateTime TanggalRilis { get; set; }

        [Required(ErrorMessage = "Jumlah Halaman wajib diisi.")]
        [Range(1, int.MaxValue, ErrorMessage = "Jumlah halaman harus lebih dari 0.")]
        public int JumlahHalaman { get; set; }

        public JenisBukuModel? JenisBuku { get; set; }
    }
}