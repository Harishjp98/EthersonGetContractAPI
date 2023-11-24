using EthersonGetContractAPI.Data;
using EthersonGetContractAPI.IRepository;
using EthersonGetContractAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EthersonGetContractAPI.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDBContext _db;
        public ContractRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Contracts Obj)
        {
            await _db.AddAsync(Obj);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Contracts>> GetAllAsync(Expression<Func<Contracts, bool>>? filter = null, bool track = true)
        {
            IQueryable<Contracts> quary = _db.contracts;
            if (filter != null)
            {
                quary = quary.Where(filter);

            }
            return await quary.ToListAsync();

        }

        public async Task<Contracts> GetAsync(Expression<Func<Contracts, bool>> filter, bool track = true)
        {
            IQueryable<Contracts> quary = _db.contracts;
            if (filter != null)
            {
                quary = quary.Where(filter);

            }
            return await quary.FirstOrDefaultAsync();
        }
    }
}
