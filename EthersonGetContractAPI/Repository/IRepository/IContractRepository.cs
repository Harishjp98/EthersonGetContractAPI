using EthersonGetContractAPI.Model;
using System.Linq.Expressions;

namespace EthersonGetContractAPI.IRepository
{
    public interface IContractRepository 
    {
        Task CreateAsync(Contracts Entity);
        Task<Contracts> GetAsync(Expression<Func<Contracts, bool>> filter, bool track = true);
        Task<List<Contracts>> GetAllAsync(Expression<Func<Contracts, bool>>? filter = null, bool track = true);
    }
    
}
