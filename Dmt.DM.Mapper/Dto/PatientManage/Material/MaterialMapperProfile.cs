using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Material
{
    public class MaterialMapperProfile : Profile
    {
        public MaterialMapperProfile()
        {
            CreateMap<MaterialDto, MaterialEntity>();
        }
    }
}
