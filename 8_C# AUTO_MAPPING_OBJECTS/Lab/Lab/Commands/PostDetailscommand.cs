using AutoMapper;
using Forum.App.Commands.Contracts;
using Forum.App.Models;
using Forum.Services.Contracts;
using System.Linq;
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

            // manuell witout automapper
            //------------------------------------------------------
            //var postDto = new PostDetailsDto
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    Content = post.Content,
            //    AuthorUsername = post.Author.Username,
            //    Replies = post.Replies.Select(r => new ReplyDto
            //    {
            //        Content = r.Content,
            //        AuthorUsername = r.Author.Username
            //    })
            //};
            //-----------------------------------------------------

            // with automapper
            //-------------------------------------------------
            var postDto = Mapper.Map<PostDetailsDto>(post);
            //--------------------------------------------------

            var sb = new StringBuilder();
                
            sb.AppendLine($"{postDto.Title} by {postDto.AuthorUsername}");

            foreach (var x in postDto.Replies)
            {
                sb.AppendLine($"----{x.Content} by {x.AuthorUsername}");
            }

            return sb.ToString().Trim();
        }
    }
}
