using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController(IMongoClient client) : ControllerBase {
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScheduleRequestDto scheduleRequest) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var collection = client.GetDatabase("StudyPal").GetCollection<Models.Schedule>("schedules");
        List<Schedule> parsed = scheduleRequest.Schedule.Select(elem => new Schedule
            { TimeFrom = elem.TimeFrom, TimeTo = elem.TimeTo, UserId = scheduleRequest.UserId }).ToList();

        await collection
            .InsertManyAsync(parsed);

        return Ok(parsed.Select(elem => elem.Id).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var collection = client.GetDatabase("StudyPal").GetCollection<Models.Schedule>("schedules");

        var results = await collection.FindAsync(elem => elem.UserId == id);

        return Ok(results.ToList());
    }

    /// <summary>
    /// get the timeframes for the current week for given user
    /// </summary>
    /// <returns>list of Schedules</returns>
    [HttpGet("week/{id}")]
    public async Task<IActionResult> GetCurrentWeek(Guid id) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var weekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);

        var theWeeknd = weekStart.AddDays(7).AddSeconds(-1);

        var collection = client.GetDatabase("StudyPal").GetCollection<Models.Schedule>("schedules");
        var results = await collection.FindAsync(elem =>
                elem.UserId == id &&
                elem.TimeFrom >= ((DateTimeOffset)weekStart).ToUnixTimeMilliseconds() &&
                elem.TimeTo <=
                ((DateTimeOffset)theWeeknd).ToUnixTimeMilliseconds() // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }
}