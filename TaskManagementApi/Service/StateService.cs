using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApi.Models;

namespace TaskManagementApi.Service
{
    public class StateService : IStateService
    {
        private readonly TaskManagementContext _context;

        public StateService(TaskManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<State> GetStateByIdAsync(int id)
        {
            return await _context.States.FindAsync(id);
        }

        public async Task<State> CreateStateAsync(State state)
        {
            _context.States.Add(state);
            await _context.SaveChangesAsync();
            return state;
        }

        public async Task<bool> UpdateStateAsync(State state)
        {
            _context.Entry(state).State = EntityState.Modified;
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null) return false;

            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
