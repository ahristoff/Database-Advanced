namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
            
            using (PhotoShareContext context = new PhotoShareContext())
            {
                string albumName = data[1];
                string tagName = data[2];

                if (context.Albums.Any(a => a.Name != albumName) || context.Tags.Any(t => t.Name != tagName))
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);
                var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

                var albumTag = new AlbumTag()
                {
                    Album = album,
                    Tag = tag

                };

                album.AlbumTags.Add(albumTag);

                context.SaveChanges();

                return $"Tag{tag} added to {album}!";
            }
        }
    }
}
