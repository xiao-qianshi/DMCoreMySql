using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.Area
{
    public class AreaMapperProfile : Profile
    {
        public AreaMapperProfile()
        {
            CreateMap<AreaDto, AreaEntity>();
        }
    }
}
