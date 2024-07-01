using PersonManagement.Helper;
using PersonManagement.Model;
using PersonManagement.Repos.Models;

namespace PersonManagement.Service
{
    public interface IPersonService
    {
        Task<List<Person>> GetAll(int code);
        Task<Person> GetByCode(int code);
        Task<APIResponse> Remove(int code);
        Task<APIResponse> Create(Person data);

        Task<APIResponse> Update(Person data,int code);
    }
}
