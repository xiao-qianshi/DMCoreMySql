using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.User
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDto, UserEntity>();
            //CreateMap<UpdateUserDto, UserEntity>()
            //    .ForMember(dest => dest.F_Birthday, opt => opt.Condition(src => !src.F_Birthday.Equals("&nbsp;",StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
