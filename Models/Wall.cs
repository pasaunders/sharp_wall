using System.ComponentModel.DataAnnotations;

namespace wall.Models
{

    public class Message
    {
        [Required]
        [MinLength(1)]
        public string message {get; set;}
    }
    public class Comment
    {
        [Required]
        [MinLength(1)]
        public string comment {get; set;}
        [Required]
        public int messagesId {get; set;}
    }
}