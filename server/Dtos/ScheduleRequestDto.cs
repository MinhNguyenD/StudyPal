using System.ComponentModel.DataAnnotations;

namespace server;

public record TimeFrame(long TimeFrom, long TimeTo);

public class ScheduleRequestDto {
    [Required] public List<TimeFrame> Schedule;
    [Required] public Guid UserId;
}