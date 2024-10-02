using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApi.Models;

namespace TaskManagementApi.Service
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStatesAsync();
        Task<State> GetStateByIdAsync(int id);
        Task<State> CreateStateAsync(State state);
        Task<bool> UpdateStateAsync(State state);
        Task<bool> DeleteStateAsync(int id);
    }
}
