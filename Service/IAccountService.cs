using PersonManagement.Helper;
using PersonManagement.Model;
using PersonManagement.Repos.Models;

namespace PersonManagement.Service
{
    public interface IAccountService
    {
        Task<List<Account>> GetAll(int pcode);
        Task<List<AccountStatus>>  GetAllAccountStatuses();
        Task<Account> GetByCode(int code);
        Task<APIResponse> Remove(int code);
        Task<APIResponse> Create(Account data);

        Task<APIResponse> Update(Account data,int code);
    }
}
