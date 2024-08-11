using System.ComponentModel.DataAnnotations;

namespace server.Dtos
{
    public class TutorScheduleDto
    {
        public string eventId { get; set; }
        public string username { get; set; }
        public string courseId { get; set; }
        [Required] public List<TimeFrame> Schedule { get; set; }
        public List<string> users { get; set; }   
    }
}
