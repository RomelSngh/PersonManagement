using PersonManagement.Helper;
using PersonManagement.Model;
using PersonManagement.Repos.Models;
using System.Transactions;

namespace PersonManagement.Service
{
    public interface ITransactionService
    {
        Task<List<Repos.Models.Transaction>> GetAll(int code);
        Task<Repos.Models.Transaction> GetByCode(int code);
        Task<APIResponse> Remove(int code);
        Task<APIResponse> Create(Repos.Models.Transaction data);

        Task<APIResponse> Update(Repos.Models.Transaction data,int code);
    }
}
