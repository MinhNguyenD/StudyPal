using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace server.Controllers; 

[ApiController]
[Route("schedule")]
public class Schedule(IMongoClient client) : ControllerBase {
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScheduleDto schedule) {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await client.GetDatabase("StudyPal").GetCollection<ScheduleDto>("schedules").InsertOneAsync(schedule);
        
        return Ok(schedule);
    }
}