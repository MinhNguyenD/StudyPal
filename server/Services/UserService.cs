using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server.Dtos;
using server.Models;
using MongoDB.Bson;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public UserService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            var mongoClient = new MongoClient(_config["MongoDB:AtlasURI"]);
            var mongoDatabase = mongoClient.GetDatabase(_config["MongoDB:DatabaseName"]);
            _userCollection = mongoDatabase.GetCollection<User>("Users");
            _userManager = userManager;
        }

        public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string username) =>
            await _userCollection.Find(x => x.UserName == username).FirstOrDefaultAsync();
 
        public async Task<List<string>> GetRoleAsync(string username)
        {
            var currentUser = await _userManager.FindByNameAsync(username);
            if (currentUser == null)
            {
                return null;
            }

            var userRoles = await _userManager.GetRolesAsync(currentUser);
            List<string> roles = new List<string>();
            foreach (var role in userRoles)
            {
                roles.Add(role);
            }
            return roles;
        }

        public async Task UpdateAsync(string username,UpdateUserDto updateUserDto)
        {
            var currentUser = await _userManager.FindByNameAsync(username);
            var university = updateUserDto.University;
            var major = updateUserDto.Major;

            if (currentUser == null)
            {
                return;
            }

            currentUser.FirstName = updateUserDto.FirstName;
            currentUser.LastName = updateUserDto.LastName;

            if (!(university == "")) {
                currentUser.University = university;
                var updateUni = Builders<User>.Update.Set(x => x.University, university);
                var filterUni = Builders<User>.Filter.Eq(x => x.UserName, username);
                await _userCollection.UpdateOneAsync(filterUni, updateUni);
            }
            if (!(major == ""))
            {
                currentUser.Major = major;
                var updateMajor = Builders<User>.Update.Set(x => x.Major, major);
                var filterMajor = Builders<User>.Filter.Eq(x => x.UserName, username);
                await _userCollection.UpdateOneAsync(filterMajor, updateMajor);
            }

            var roles = updateUserDto.Roles;
            if (!(roles.Count == 0))
            {
                List<string> currentRoles = await GetRoleAsync(username);
                foreach (string oldRole in currentRoles)
                {
                    var removeRoles = await _userManager.RemoveFromRoleAsync(currentUser, oldRole);
                }

                foreach (string role in roles)
                {
                    var roleResult = await _userManager.AddToRoleAsync(currentUser, role);
                }
            }

            await _userCollection.ReplaceOneAsync(x => x.UserName == username, currentUser);
        }

        public async Task UpdatePasswordAsync(string username, UpdatePasswordDto updatePasswordDto)
        {
            var currentUser = await _userManager.FindByNameAsync(username);
            if (currentUser == null)
            {
                return;
            }
            var result = await _userManager.ChangePasswordAsync(currentUser, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);

            await _userCollection.ReplaceOneAsync(x => x.UserName == username, currentUser);
        }

        public async Task<bool> VerifyPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task AddCourse(string username, Course newCourse)
        {
            var currentUser = await _userManager.FindByNameAsync(username);
            if (currentUser == null)
            {
                return;
            }

            List<Course> courseList = currentUser.Courses;

            if (!(courseList == null))
            {
                foreach (Course course in courseList)
                {
                    if (newCourse.CourseCode == course.CourseCode)
                    {
                        return;
                    }
                }
            }
            else
            {
                // create the array field for courses if value is null
                var checkNull = Builders<User>.Filter.Exists(x => x.Courses, false) | Builders<User>.Filter.Eq("Course", BsonNull.Value);
                var updateArray = Builders<User>.Update.Set("Courses", new BsonArray());
                await _userCollection.UpdateManyAsync(checkNull, updateArray);

                // add the courses
                var filterCourse = Builders<User>.Filter.Eq(x => x.UserName, username);
                var updateCourse = Builders<User>.Update.Push(x => x.Courses, newCourse);
                await _userCollection.UpdateOneAsync(filterCourse, updateCourse);
                return;
            }
            courseList.Add(newCourse);
            await _userCollection.ReplaceOneAsync(x => x.UserName == username, currentUser);
        }

        public async Task RemoveAsync(string username) =>
            await _userCollection.DeleteOneAsync(x => x.UserName == username);
    }
}
