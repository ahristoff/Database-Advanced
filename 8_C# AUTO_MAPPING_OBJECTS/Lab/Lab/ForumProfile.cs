using AutoMapper;
using Forum.App.Models;
using Lab.Data.Models;

namespace Forum.App
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            CreateMap<Post, PostDetailsDto>()
               .ForMember(d => d.ReplyCount,
                          u => u.MapFrom(p => p.Replies.Count));
            CreateMap<Reply, ReplyDto>();
        }
    }
}
