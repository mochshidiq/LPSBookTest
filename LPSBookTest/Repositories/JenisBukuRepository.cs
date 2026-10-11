using LPSBookTest.Data;
using LPSBookTest.Models;

namespace LPSBookTest.Repositories
{
    public class JenisBukuRepository
        : Repository<JenisBukuModel>, IJenisBukuRepository
    {
        public JenisBukuRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}