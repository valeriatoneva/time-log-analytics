using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
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

        [HttpGet]
public async Task<IActionResult> GetTimeLogs([FromQuery] int pageIndex, [FromQuery] int pageSize)
{
    // Calculate the number of records to skip (offset)
    int skip = (pageIndex - 1) * pageSize;
    
    // Query the database for the total count of time logs
    int totalRecords = await _context.TimeLogs.CountAsync();
    
    // Retrieve the specified page of time logs, ordering by date
    var timeLogs = await _context.TimeLogs
                                .OrderBy(tl => tl.Date) // Sorting by Date
                                .Skip(skip)  // Skip the records before the current page
                                .Take(pageSize) // Take the records for the current page
                                .Select(tl => new 
                                {
                                    TimeLogId = tl.TimeLogId,
                                    UserId = tl.UserId,
                                    ProjectId = tl.ProjectId,
                                    Date = tl.Date,
                                    HoursWorked = tl.HoursWorked
                                })
                                .ToListAsync();
    
    // Return the paged data and total record count to the client
    return Ok(new { TotalRecords = totalRecords, Data = timeLogs });
}
}
}