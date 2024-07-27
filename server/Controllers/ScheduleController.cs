using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController(IMongoClient client) : ControllerBase {
    private readonly IMongoCollection<Schedule> _schedules = client.GetDatabase("StudyPal").GetCollection<Schedule>("schedules");

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScheduleRequestDto scheduleRequest) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        List<Schedule> parsed = scheduleRequest.Schedule.Select(elem => new Schedule {
                TimeFrom = elem.TimeFrom, TimeTo = elem.TimeTo, UserId = Guid.Parse(userId),
                CourseId = scheduleRequest.CourseId
            })
            .Where(elem => elem.TimeFrom < elem.TimeTo).ToList();
        // TODO: check for overlaps in timeframes
        await _schedules
            .InsertManyAsync(parsed);

        return Ok(parsed.Select(elem => elem.Id).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var results = await _schedules.FindAsync(elem => elem.UserId == id);

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

        var results = await _schedules.FindAsync(elem =>
                elem.UserId == id &&
                elem.TimeFrom >= ((DateTimeOffset)weekStart).ToUnixTimeMilliseconds() &&
                elem.TimeTo <=
                ((DateTimeOffset)theWeeknd).ToUnixTimeMilliseconds() // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }

    [HttpGet("week/course/{courseId}")]
    public async Task<IActionResult> GetCurrentWeekByCourse(string courseId) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var weekStart = DateTime.Today.AddDays(-(int)DateTime.Now.DayOfWeek);

        var theWeeknd = weekStart.AddDays(7);

        var results = await _schedules.FindAsync(elem =>
                elem.CourseId == courseId &&
                elem.TimeFrom >= ((DateTimeOffset)weekStart).ToUnixTimeMilliseconds() &&
                elem.TimeTo <=
                ((DateTimeOffset)theWeeknd).ToUnixTimeMilliseconds() // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }

    [HttpGet("week/user/{userId}/course/{courseId}")]
    public async Task<IActionResult> GetCurrentWeekByCourse(string courseId, Guid userId) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        var weekStart = DateTime.Today.AddDays(-(int)DateTime.Now.DayOfWeek);

        var theWeeknd = weekStart.AddDays(7);

        var results = await _schedules.FindAsync(elem =>
                elem.UserId == userId &&
                elem.CourseId == courseId &&
                elem.TimeFrom >= ((DateTimeOffset)weekStart).ToUnixTimeMilliseconds() &&
                elem.TimeTo <=
                ((DateTimeOffset)theWeeknd).ToUnixTimeMilliseconds() // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }
    
    [HttpDelete("delete/{scheduleId}")]
    public async Task<IActionResult> Delete(string scheduleId) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var result = await _schedules.DeleteOneAsync(schedule => schedule.Id == ObjectId.Parse(scheduleId));
        if (result.DeletedCount == 1) {
            return Ok();
        }

        return NotFound();
    }
}