using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamBuilder.Models
{
    public class User
    {
        public User()
        {
            IsDeleted = false;
        }
        public int Id { get; set; }
        [MinLength(3)]
        public string UserName { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Invitation> ReceivedInvitations { get; set; } = new List<Invitation>();

        public ICollection<Team> CreatedTeams { get; set; } = new List<Team>();

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();
    }

    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}
