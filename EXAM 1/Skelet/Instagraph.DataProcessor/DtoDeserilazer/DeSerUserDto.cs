using Instagraph.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.DtoDeserilazer
{
    public class DeSerUserDto
    {      
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
    }
}
