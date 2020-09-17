using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.DialysisMachine
{
    public class DialysisMachineMapperProfile : Profile
    {
        public DialysisMachineMapperProfile()
        {
            CreateMap<DialysisMachineDto, DialysisMachineEntity>();
        }
    }
}
