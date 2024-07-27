using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/course")]
public class CourseController(IMongoClient client) : ControllerBase {
    private readonly IMongoCollection<Course> _courses =
        client.GetDatabase("StudyPal").GetCollection<Course>("courses");

    public class CourseRequest {
        public string name { get; set; }
        public string code { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourseRequest courseRequest) {
        if (!ModelState.IsValid) {
            return BadRequest();
        }

        var courses = await _courses.Find(_ => true).ToListAsync();
        if (courses.Any(course => string.Equals(course.CourseCode, courseRequest.code, StringComparison.CurrentCultureIgnoreCase)))
        {
            return Conflict();
        }
        var course = new Course { CourseCode = courseRequest.code.ToUpper(), CourseName = courseRequest.name };
        await _courses.InsertOneAsync(course);

        return Ok(course.Id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        if (!ModelState.IsValid) {
            return BadRequest();
        }
        var courses = await _courses.Find(_ => true).ToListAsync();
        return Ok(courses);
    }
}