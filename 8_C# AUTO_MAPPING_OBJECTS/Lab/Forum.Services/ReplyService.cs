using Forum.Services.Contracts;
using System;
using Lab.Data.Models;
using Forum.Data;
using System.Linq;

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
            var reply = context.Replies.FirstOrDefault(r => r.Id == replyId);

            if (reply == null)
            {
                throw new ArgumentException("No such reply");
            }

            context.Replies.Remove(reply);
            context.SaveChanges();
        }
    }
}
