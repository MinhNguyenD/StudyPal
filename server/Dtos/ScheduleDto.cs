using System.ComponentModel.DataAnnotations;

namespace server; 

public class ScheduleDto {
    [Required] public long TimeFrom;
    [Required] public long TimeTo;
}