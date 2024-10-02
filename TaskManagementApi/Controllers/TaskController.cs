using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagementContext _context;

        public TaskController(TaskManagementContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppTask>>> GetTasks()
        {
            var tasks = await _context.Tasks.Include(t => t.State).ToListAsync();
            if (tasks == null || !tasks.Any())
            {
                return Ok(new List<AppTask>());
            }
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<AppTask>> PostTask(AppTask task)
        {
            if (task == null)
            {
                return BadRequest("El objeto de tarea no puede ser nulo.");
            }

            // valida que stateId sea un estado válido
            if (!_context.States.Any(s => s.Id == task.StateId))
            {
                return BadRequest($"El stateId {task.StateId} no es válido.");
            }

            // no se asigna State ya que solo se necesita stateId || State isn´t assigned since only stateId is needed
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, AppTask task)
        {
            if (id != task.Id)
            {
                return BadRequest("El ID de la tarea no coincide.");
            }

            // valida que stateId sea un estado válido || validates that stateId is a valid state
            if (!_context.States.Any(s => s.Id == task.StateId))
            {
                return BadRequest($"El stateId {task.StateId} no es válido.");
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // funcion auxiliar para verificar si la tarea existe || helper function to check if task exists
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
