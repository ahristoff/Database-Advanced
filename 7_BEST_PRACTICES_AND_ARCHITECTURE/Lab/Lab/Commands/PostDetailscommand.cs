using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System.Text;

namespace Forum.App.Commands
{
    public class PostDetailsCommand : ICommand
    {
        private readonly IPostService PostService;

        public PostDetailsCommand(IPostService PostService)
        {
            this.PostService = PostService;
        }

        public string Execute(params string[] arguments)
        {
            var postId = int.Parse(arguments[0]);

            var post = PostService.ById(postId);

            var sb = new StringBuilder();
                
            sb.AppendLine($"{post.Title} by {post.Author.Username}");

            foreach (var x in post.Replies)
            {
                sb.AppendLine($"----{x.Content} by {x.Author.Username}");
            }

            return sb.ToString();
        }
    }
}
