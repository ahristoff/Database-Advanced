namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            // TODO prints all friends of user with given username.
            string username = data[1];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                     .Include(u => u.FriendsAdded)
                        .ThenInclude(f => f.Friend)
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.FriendsAdded.Count == 0)
                {
                    return"No friends for this user. :(";
                }

                var sb = new StringBuilder();

                foreach (var x in user.FriendsAdded)
                {
                    sb.AppendLine($"{x.Friend.Username}");
                }

                return sb.ToString();
            }
        }
    }
}
