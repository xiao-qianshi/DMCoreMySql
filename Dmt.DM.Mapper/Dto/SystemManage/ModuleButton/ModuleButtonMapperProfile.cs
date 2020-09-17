using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.ModuleButton
{
    public class ModuleButtonMapperProfile : Profile
    {
        public ModuleButtonMapperProfile()
        {
            CreateMap<ModuleButtonDto, ModuleButtonEntity>();
        }
    }
}
