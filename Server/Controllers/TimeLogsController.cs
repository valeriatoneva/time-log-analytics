using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataAccess;
using Server.DataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeLogsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TimeLogsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/timelogs
        [HttpGet]
        public async Task<IActionResult> GetTimeLogs([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            int skip = (pageIndex - 1) * pageSize;
            int totalRecords = await _context.TimeLogs.CountAsync();

            var timeLogs = await _context.TimeLogs
                                        .OrderBy(tl => tl.Date) 
                                        .Skip(skip)  
                                        .Take(pageSize) 
                                        .Select(tl => new 
                                        {
                                            TimeLogId = tl.TimeLogId,
                                            UserId = tl.UserId,
                                            ProjectId = tl.ProjectId,
                                            Date = tl.Date,
                                            HoursWorked = tl.HoursWorked
                                        })
                                        .ToListAsync();

            return Ok(new { TotalRecords = totalRecords, Data = timeLogs });
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> PostTimeLog([FromBody] TimeLog timeLog)
        {
            _context.TimeLogs.Add(timeLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimeLog), new { id = timeLog.TimeLogId }, timeLog);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeLog(int id)
        {
            var timeLog = await _context.TimeLogs.FindAsync(id);

            if (timeLog == null)
            {
                return NotFound();
            }

            return Ok(timeLog);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeLog(int id, [FromBody] TimeLog updatedTimeLog)
        {
            if (id != updatedTimeLog.TimeLogId)
            {
                return BadRequest();
            }

            _context.Entry(updatedTimeLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TimeLogs.Any(e => e.TimeLogId == id))
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

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeLog(int id)
        {
            var timeLog = await _context.TimeLogs.FindAsync(id);
            if (timeLog == null)
            {
                return NotFound();
            }

            _context.TimeLogs.Remove(timeLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
