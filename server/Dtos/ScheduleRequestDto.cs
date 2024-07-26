using System.ComponentModel.DataAnnotations;

namespace server;

public record TimeFrame(long TimeFrom, long TimeTo);

public class ScheduleRequestDto {
    [Required] public List<TimeFrame> Schedule { get; set; }
    [Required] public string CourseId { get; set; }
}