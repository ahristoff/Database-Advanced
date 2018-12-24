using Forum.Data;
using Forum.Data.Models;
using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Lab
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (var context = new ForumDbContext())
            {
                ResetDatabase(context);

                var categories = context.Categories
                   .Select(c => new
                   {
                       c.Name,
                       Posts = c.Posts.Select(p => new
                       {
                           p.Title,
                           p.Content,
                           AuthorUsername = p.Author.Username,
                           Replies = p.Replies.Select(r => new
                           {
                               r.Content,
                               AuthorUsernamme = r.Author.Username
                           }).ToArray()
                       }).ToArray()
                   })
                   .ToArray();

                foreach (var x in categories)
                {
                    Console.WriteLine($"{x.Name} ({x.Posts.Count()})");

                    foreach (var post in x.Posts)
                    {
                        Console.WriteLine($"--{post.Title}: {post.Content}");
                        Console.WriteLine($"--by {post.AuthorUsername}");

                        foreach (var reply in post.Replies)
                        {
                            Console.WriteLine($"----{reply.Content} from {reply.AuthorUsernamme}");
                        }
                    }
                }
            }
        }

        private static void ResetDatabase(ForumDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();      

            Seed(context);                  
        }

        private static void Seed(ForumDbContext context)
        {
            var users = new[]
           {
                new User("Gosho", "123"),
                new User("Peshooo", "123"),
                new User("ivan", "223"),
                new User("Merryf", "123"),
            };

            context.Users.AddRange(users);


            var categories = new[]
            {
                new Category ("JavaSkript"),
                new Category("Support"),
                new Category("Pyton"),
                new Category("EF KOP")
            };
            context.Categories.AddRange(categories);

            var posts = new[]
            {
                new Post ("C#Rulz", "Вярно",categories[0], users[0]),
                new Post ("PytonRulz", "Вярно",categories[2], users[1]),
                new Post ("My Comp doesn't turn on", "Вярно",categories[1], users[2]),
                
            };
            context.Posts.AddRange(posts);

            var replies = new[]
           {
                new Reply ("Turn it on", posts[2], users[0]),
                new Reply ("Yep", posts[0], users[2]),
            };
            context.Replies.AddRange(replies);

            context.SaveChanges();
        }
    }
}
