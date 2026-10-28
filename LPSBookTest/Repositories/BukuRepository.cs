using LPSBookTest.Data;
using LPSBookTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LPSBookTest.Repositories
{
    public class BukuRepository : Repository<BukuModel>, IBukuRepository
    {
        public BukuRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<BukuModel>> GetAllWithJenisBukuAsync(
            string? search = null)
        {
            var query = _context.Bukus
                .Include(x => x.JenisBuku)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(x =>
                    x.Judul.Contains(search) ||
                    x.Penulis.Contains(search) ||
                    x.JenisBuku!.JenisBuku.Contains(search));
            }

            return await query
                .OrderBy(x => x.Judul)
                .ToListAsync();
        }
    }
}