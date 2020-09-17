using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Drugs
{
    public class DrugsMapperProfile : Profile
    {
        public DrugsMapperProfile()
        {
            CreateMap<DrugsDto, DrugsEntity>();
        }
    }
}
