using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System.Text;
using System.Linq;

namespace Forum.App.Commands
{
    public class ListPostsCommand : ICommand
    {
        private IPostService postService;

        public ListPostsCommand(IPostService postService)
        {
            this.postService = postService;
        }

        public string Execute(params string[] arguments)
        {
            var posts = postService
                .All()
                .GroupBy(p => p.Category);

            var sb = new StringBuilder();

            foreach (var x in posts)
            {
                var categoryName = x.Key.Name;
                sb.AppendLine($"categoryName: {categoryName}");

                foreach (var y in x)
                {
                    sb.AppendLine($"--{y.Id} {y.Title} - {y.Content} by {y.Author.Username}");
                }            
            }

            return sb.ToString();
        }
    }
}
