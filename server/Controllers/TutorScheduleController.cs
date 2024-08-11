using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TutorScheduleController : ControllerBase
{
    private readonly TutorScheduleService _tutorScheduleService;

    public TutorScheduleController(TutorScheduleService tutorScheduleService)
    {
        _tutorScheduleService = tutorScheduleService;
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleTutor(TutorSchedule tutorSchedule)
    {
        var result = await _tutorScheduleService.ScheduleTutorAsync(tutorSchedule);
        return CreatedAtAction(nameof(ScheduleTutor), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<TutorSchedule>>> GetAllTutorSchedules()
    {
        var schedules = await _tutorScheduleService.GetAllTutorSchedulesAsync();
        return Ok(schedules);
    }
        
    [HttpGet("{username}")]
    public async Task<ActionResult<List<TutorSchedule>>> GetSchedulesByUsername(string username)
    {
        var schedules = await _tutorScheduleService.GetSchedulesByUsernameAsync(username);
        if (schedules == null || schedules.Count == 0)
        {
            return NotFound();
        }
        return Ok(schedules);
    }

    [HttpPut("adduser/{eventId}/{username}")]
    public async Task<IActionResult> AddUserToSchedule(string eventId, string username)
    {
        var result = await _tutorScheduleService.AddUserToScheduleAsync(eventId, username);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpPut("removeuser/{eventId}/{username}")]
    public async Task<IActionResult> RemoveUserFromSchedule(string eventId, string username)
    {
        var result = await _tutorScheduleService.RemoveUserFromScheduleAsync(eventId, username);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("delete/{eventId}")]
    public async Task<IActionResult> DeleteTutorSchedule(string eventId)
    {
        var result = await _tutorScheduleService.DeleteTutorScheduleAsync(eventId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }
}
