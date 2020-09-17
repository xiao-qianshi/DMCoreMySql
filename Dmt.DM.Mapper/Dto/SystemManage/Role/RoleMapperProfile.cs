using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.Role
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<RoleDto, RoleEntity>();
        }
    }
}
