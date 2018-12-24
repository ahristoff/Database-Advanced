namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string username = data[1];
            string albumtitle = data[2];
            string colorStr = data[3];

            var tags = new List<string>();
            for (int i = 4; i < data.Length; i++)
            {
                tags.Add(data[i]);
            }

            var color = Enum.Parse<Color>(colorStr);

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
             
                if (context.Albums.Any(u => u.Name == albumtitle))
                {
                    throw new ArgumentException($"Album {albumtitle} exists!");
                }
                
                var colors = context.Albums                    
                    .Where(a => a.BackgroundColor != null);

                if (colors == null)
                {
                    throw new ArgumentException($"Color {color} not found!");
                }

                var album = new Album
                {
                    Name = albumtitle,
                    BackgroundColor = color,
                    // AlbumTags
                };

                var albumTags = new List<AlbumTag>();

                foreach (var x in tags)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == x);
                    if (tag != null)
                    {
                        albumTags.Add(
                            new AlbumTag()
                            {
                                Album = album,
                                Tag = tag
                            });   
                    }
                }

                if (tags == null)
                {
                    throw new ArgumentException("Invalid tags!");
                }

                context.Albums.Add(album);
                context.SaveChanges();

                return $"Album {album} successfully created!";
            }
        }
    }
}
