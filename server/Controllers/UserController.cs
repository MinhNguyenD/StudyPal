using server.Models;
using server.Services;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace server.Controllers
{
    [ApiController]
    [Route("update/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        //private readonly CourseService _courseService;
        public UserController(UserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<List<User>> Get() => await _userService.GetAsync();

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> Get(string username)
        {
            var user = await _userService.GetAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userService.UpdateAsync(username, updateUserDto);

            return NoContent();
        }

        [HttpPut("password/{username}")]
        public async Task<IActionResult> UpdatePassword(string username, [FromBody] UpdatePasswordDto updatePasswordDto)
        {
            var verified = await _userService.VerifyPasswordAsync(username, updatePasswordDto.CurrentPassword);
            if (verified == false)
            {
                return Unauthorized("Password is incorrect!");
            }

            await _userService.UpdatePasswordAsync(username, updatePasswordDto);

            return NoContent();
        }

        [HttpPut("course/{username}")]
        public async Task<IActionResult> UpdateCourse(string username, [FromBody] CourseDto courseDto)
        {
            var newCourse = new Course()
            {
                CourseCode = courseDto.CourseCode,
                CourseName = courseDto.CourseName
            };
            //await _courseService.AddCourse(newCourse);
            await _userService.AddCourse(username, newCourse);
            
            return NoContent();

        }
    }
}
