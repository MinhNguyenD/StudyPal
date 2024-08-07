using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController(IMongoClient client) : ControllerBase {
    private readonly IMongoCollection<Schedule> _schedules =
        client.GetDatabase("StudyPal").GetCollection<Schedule>("schedules");

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

    [HttpGet("schedules/{id}")]
    public async Task<IActionResult> GetSchedule(string id) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var results = await _schedules.FindAsync(elem => elem.Id.ToString().Equals(id));

        return Ok(results.ToList());
    }

    /// <summary>
    /// get the timeframes for the current week for given user
    /// </summary>
    /// <returns>list of Schedules</returns>
    [HttpGet("week/{id}/{start}/{end}")]
    public async Task<IActionResult> GetCurrentWeek(Guid id, long start, long end) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var weekStart = new DateTime(start);
        // var weekStart = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

        // var theWeeknd = weekStart.AddDays(7).AddSeconds(-1);
        var theWeeknd = new DateTime(end);
        var results = await _schedules.FindAsync(elem =>
                elem.UserId == id &&
                elem.TimeFrom >= start &&
                elem.TimeTo <=
                end // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }

    [HttpGet("week/course/{courseId}/{start}/{end}")]
    public async Task<IActionResult> GetCurrentWeekByCourse(string courseId, long start, long end) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        // var week = new DateTime(start);
        // var weekStart = new DateTime(start);
        // var weekStart = DateTime.Now.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

        // var theWeeknd = new DateTime(end);

        var results = await _schedules.FindAsync(elem =>
                elem.CourseId == courseId &&
                elem.TimeFrom >= start &&
                elem.TimeTo <=
                end // get all timeframes clamped to this week
        );

        return Ok(results.ToList());
    }

    [HttpGet("week/user/course/{courseId}/{start}/{end}")]
    public async Task<IActionResult> GetCurrentWeekByUser(string courseId, long start, long end) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        // var week = new DateTime(start);
        var weekStart = new DateTime(start);
        // var weekStart = DateTime.Now.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);

        var theWeeknd = new DateTime(end);
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) {
            return Forbid();
        }

        var results = await _schedules.FindAsync(elem =>
                elem.UserId == Guid.Parse(userId) &&
                elem.CourseId == courseId &&
                elem.TimeFrom >= start &&
                elem.TimeTo <=
                end // get all timeframes clamped to this week
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

    [HttpDelete("delete/{timeFrom}/{timeTo}/users/{userId}")]
    public async Task<IActionResult> DeleteFromUser(long timeFrom, long timeTo, string userId) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        var result = await _schedules.DeleteOneAsync(schedule => schedule.UserId == Guid.Parse(userId) && schedule.TimeFrom == timeFrom && schedule.TimeTo == timeTo);
        if (result.DeletedCount == 1) {
            return Ok();
        }

        return NotFound();
    }
}