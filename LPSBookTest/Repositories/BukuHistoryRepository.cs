using LPSBookTest.Data;
using LPSBookTest.Models;

namespace LPSBookTest.Repositories
{
    public class BukuHistoryRepository
        : Repository<BukuHistoryModel>,
          IBukuHistoryRepository
    {
        public BukuHistoryRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
