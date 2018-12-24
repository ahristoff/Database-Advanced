﻿namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albumId = int.Parse(data[1]);
            string username = data[2];
            Role permission = Enum.Parse<Role>(data[3]);

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (!context.Albums.Any(a => a.Id == albumId))
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                if (!context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (permission.ToString() != "Owner" || permission.ToString() != "Viewer")
                {
                    throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);
                var album = context.Albums.FirstOrDefault(a => a.Id == albumId);

                var albumRole = new AlbumRole()
                {
                    Album = album,
                    User = user,
                    Role = permission
                };

                user.AlbumRoles.Add(albumRole);

                return $"Username {username} added to album {album.Name} {permission.ToString()}";
            }
        }
    }
}
