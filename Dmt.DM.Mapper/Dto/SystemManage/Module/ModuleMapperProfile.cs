using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.Module
{
    public class ModuleMapperProfile : Profile
    {
        public ModuleMapperProfile()
        {
            CreateMap<ModuleDto, ModuleEntity>();
        }
    }
}
