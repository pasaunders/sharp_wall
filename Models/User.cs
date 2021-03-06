using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class Index
    {
        public Login login {get;set;}
        public User user {get;set;}
    }
    public class User
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"\w+")]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password")]
        public string confirmPassword { get; set; }
    }
    public class Login
    {
        [Required]
        public string name {get;set;}
        public string password {get;set;}
    }
}