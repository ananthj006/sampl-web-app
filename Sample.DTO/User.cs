using System.ComponentModel.DataAnnotations;

namespace Sample.DTO
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
    }
}
