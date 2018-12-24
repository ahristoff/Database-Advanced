namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = data[1];
            string pictureTitle = data[2];
            string pictureFilePath = data[3];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums
                    .FirstOrDefault(a => a.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException($"Album {album} not found!");
                }

                var pict = new Picture()
                {
                    Title = pictureTitle,
                    Path = pictureFilePath
                };

                album.Pictures.Add(pict);
                             
                db.SaveChanges();

                return $"Picture {pict} added to {album}!";
            }
        }
    }
}
