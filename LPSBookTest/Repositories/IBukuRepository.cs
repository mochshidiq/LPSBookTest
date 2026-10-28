using LPSBookTest.Models;

namespace LPSBookTest.Repositories
{
    public interface IBukuRepository : IRepository<BukuModel>
    {
        Task<IEnumerable<BukuModel>> GetAllWithJenisBukuAsync(string? search = null);
    }
}