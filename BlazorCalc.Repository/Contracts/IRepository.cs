using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCalc.Repository.Contracts
{
    public interface IRepository
    {
        Task<IEnumerable<string>> GetAll();
        Task Save(string expression, string result);
    }
}