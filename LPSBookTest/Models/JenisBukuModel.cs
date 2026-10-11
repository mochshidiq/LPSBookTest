using System.ComponentModel.DataAnnotations;

namespace LPSBookTest.Models
{
    public class JenisBukuModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jenis Buku wajib diisi.")]
        [StringLength(
            50,
            ErrorMessage = "Jenis Buku maksimal 50 karakter.")]
        public string JenisBuku { get; set; } = string.Empty;

        public ICollection<BukuModel> Bukus { get; set; }
            = new List<BukuModel>();
    }
}