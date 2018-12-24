namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string requerUsername = data[1];
            string addedFriendUsername = data[2];

            using (var context = new PhotoShareContext())
            {
                var requestinguser = context.Users
                     .Include(u => u.FriendsAdded)
                        .ThenInclude(f => f.Friend)
                    .FirstOrDefault(u => u.Username == requerUsername);

                if (requestinguser == null)
                {
                    throw new ArgumentException($"{requerUsername} not found!");
                }

                var addedFriend = context.Users
                    .Include(u => u.FriendsAdded)
                        .ThenInclude(f => f.Friend)
                    .FirstOrDefault(u => u.Username == addedFriendUsername);

                if (addedFriend == null)
                {
                    throw new ArgumentException($"{addedFriendUsername} not found!");
                }

                bool alreadyAdded = requestinguser.FriendsAdded.Any(u => u.Friend == addedFriend);
                bool accepted = addedFriend.FriendsAdded.Any(u => u.Friend == requestinguser);

                if (alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{addedFriendUsername} is already a friend to {requerUsername}");
                }

                if (!alreadyAdded && !accepted)
                {
                    throw new InvalidOperationException($"{requerUsername} has not added {addedFriendUsername} as a friend");
                }

                return $"{requerUsername} accepted {addedFriendUsername} as a friend";
            }
        }
    }
}
