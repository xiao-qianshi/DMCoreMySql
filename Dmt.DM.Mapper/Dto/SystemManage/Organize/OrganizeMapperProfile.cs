using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.Organize
{
    public class OrganizeMapperProfile: Profile
    {
        public OrganizeMapperProfile()
        {
            CreateMap<OrganizeDto, OrganizeEntity>();
        }
    }
}
