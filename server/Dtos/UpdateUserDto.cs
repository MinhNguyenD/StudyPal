using System.ComponentModel.DataAnnotations;

namespace server.Dtos
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public List<string> Roles { get; set; }
    }
}
