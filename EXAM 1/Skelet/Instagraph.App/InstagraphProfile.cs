using AutoMapper;
using Instagraph.DataProcessor.DtoDeserilazer;

namespace Instagraph.App
{
    public class InstagraphProfile : Profile
    {
        public InstagraphProfile()
        {
            CreateMap<DeSerUserDto, User>()
                .ForMember(u => u.ProfilePicture, pp => pp.UseValue<Picture>(null));

            CreateMap<UserFollowerDto, UserFollower>();

            CreateMap<Post, uncommentedDto>()
               .ForMember(dto => dto.Picture, pp => pp.MapFrom(p => p.Picture.Path))
               .ForMember(dto => dto.User, u => u.MapFrom(p => p.User.Username));
        }
    }
}
