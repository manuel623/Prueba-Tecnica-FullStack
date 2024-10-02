using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApi.Models;
using TaskManagementApi.Service;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        // GET: api/state
        [HttpGet]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                var states = await _stateService.GetStatesAsync();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // POST: api/state
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        {
            try
            {
                var createdState = await _stateService.CreateStateAsync(state);
                return CreatedAtAction(nameof(GetState), new { id = createdState.Id }, createdState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT: api/state/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, State state)
        {
            if (id != state.Id)
            {
                return BadRequest();
            }

            try
            {
                var updated = await _stateService.UpdateStateAsync(state);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE: api/state/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            try
            {
                var deleted = await _stateService.DeleteStateAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private async Task<State> GetState(int id)
        {
            return await _stateService.GetStateByIdAsync(id);
        }
    }
}
