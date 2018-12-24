using Forum.Services.Contracts;
using Lab.Data.Models;
using Forum.Data;

namespace Forum.Services
{
    public class ReplyService : IReplyService
    {
        private readonly ForumDbContext context;

        public ReplyService(ForumDbContext context)
        {
            this.context = context;
        }

        public Reply Create(string replyText, int postId, int authorId)
        {
            var reply = new Reply()
            {
                Content = replyText,
                PostId = postId,
                AuthorId = authorId
            };

            context.Replies.Add(reply);
            context.SaveChanges();

            return reply;
        }

        public void Delete(int replyId)
        {
            var reply = context.Replies.Find(replyId);

            context.Replies.Remove(reply);

            context.SaveChanges();
        }
    }
}
