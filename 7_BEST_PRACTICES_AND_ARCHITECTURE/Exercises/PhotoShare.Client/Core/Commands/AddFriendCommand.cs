namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
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


                if (alreadyAdded && !accepted)
                {
                    throw new InvalidOperationException("Friend request already sent");
                }

                if (alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{addedFriendUsername} is already a friend to {requerUsername}");
                }

                if (!alreadyAdded && accepted)
                {
                    throw new InvalidOperationException($"{requerUsername} has already received a friend request from {addedFriendUsername}");
                }

                requestinguser.FriendsAdded.Add(new Friendship()
                {
                    User = requestinguser,
                    Friend = addedFriend

                });

                context.SaveChanges();

                return $"Friend {addedFriendUsername} added to {requerUsername}";
            }
        }
    }
}
