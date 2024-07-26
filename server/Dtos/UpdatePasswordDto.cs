using System.ComponentModel.DataAnnotations;

namespace server.Dtos
{
    public class UpdatePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
