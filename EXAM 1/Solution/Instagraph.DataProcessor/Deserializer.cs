﻿using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;
using AutoMapper;
using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.DtoModels;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            Picture[] deserializedPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var pictures = new List<Picture>();

            foreach (var x in deserializedPictures)
            {
                bool isValid = !String.IsNullOrWhiteSpace(x.Path) && x.Size > 0;

                bool pictureExists = context.Pictures.Any(p => p.Path == x.Path) ||
                    pictures.Any(p => p.Path == x.Path);

                if (!isValid || pictureExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                pictures.Add(x);
                sb.AppendLine(String.Format(successMsg, $"Picture {x.Path}"));
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            UserDto[] deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var users = new List<User>();

            foreach (var x in deserializedUsers)
            {
                bool isValid = !String.IsNullOrWhiteSpace(x.Username) &&
                    x.Username.Length <= 30 &&
                    !String.IsNullOrWhiteSpace(x.Password) &&
                    x.Password.Length <= 20 &&
                    !String.IsNullOrWhiteSpace(x.ProfilePicture);

                var picture = context.Pictures.FirstOrDefault(p => p.Path == x.ProfilePicture);

                bool userExists = users.Any(u => u.Username == x.Username);

                if (!isValid || picture == null || userExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = Mapper.Map<User>(x);
                user.ProfilePicture = picture;

                users.Add(user);
                sb.AppendLine(String.Format(successMsg, $"User {user.Username}"));
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var deserializedFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var followers = new List<UserFollower>();

            foreach (var x in deserializedFollowers)
            {
                int? userId = context.Users.FirstOrDefault(u => u.Username == x.User)?.Id; //
                int? followerId = context.Users.FirstOrDefault(u => u.Username == x.Follower)?.Id;

                if (userId == null || followerId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                bool alreadyFollowed = followers.Any(f => f.UserId == userId && f.FollowerId == followerId);
                if (alreadyFollowed)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    UserId = userId.Value,
                    FollowerId = followerId.Value
                };

                followers.Add(userFollower);
                sb.AppendLine(String.Format(successMsg, $"Follower {x.Follower} to User {x.User}"));
            }

            context.UsersFollowers.AddRange(followers);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var posts = new List<Post>();

            foreach (var x in elements)
            {
                string caption = x.Element("caption")?.Value;
                string username = x.Element("user")?.Value;
                string picturePath = x.Element("picture")?.Value;

                bool inputIsValid = !String.IsNullOrWhiteSpace(caption) &&
                    !String.IsNullOrWhiteSpace(username) &&
                    !String.IsNullOrWhiteSpace(picturePath);

                if (!inputIsValid)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                int? userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                int? pictureId = context.Pictures.FirstOrDefault(p => p.Path == picturePath)?.Id;

                if (userId == null || pictureId == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var post = new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = pictureId.Value

                };

                posts.Add(post);
                sb.AppendLine(String.Format(successMsg, $"Post {caption}"));
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;            
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var comments = new List<Comment>();

            foreach (var x in elements)
            {
                string content = x.Element("content")?.Value;
                string userName = x.Element("user")?.Value;
                string postIdString = x.Element("post")?.Attribute("id")?.Value;

                bool noNullInput = !String.IsNullOrWhiteSpace(content) &&
                    !String.IsNullOrWhiteSpace(userName) &&
                    !String.IsNullOrWhiteSpace(postIdString);

                if (!noNullInput)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                int postId = int.Parse(postIdString);

                int? userId = context.Users.FirstOrDefault(u => u.Username == userName)?.Id;
                bool postExists = context.Posts.Any(p => p.Id == postId);

                if (userId == null || !postExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var comment = new Comment()
                {
                    Content = content,
                    UserId = userId.Value,
                    PostId = postId
                };

                comments.Add(comment);
                sb.AppendLine(String.Format(successMsg, $"Comment {content}"));
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
