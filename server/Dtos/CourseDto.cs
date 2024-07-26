using MongoDB.Bson.Serialization.Attributes;

namespace server.Dtos
{
    public class CourseDto
    {
        public string CourseCode { get; set; }

        public string CourseName { get; set; }
    }
}
