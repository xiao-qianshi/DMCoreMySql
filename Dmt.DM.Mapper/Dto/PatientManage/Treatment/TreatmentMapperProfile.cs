using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Treatment
{
    public class TreatmentMapperProfile : Profile
    {
        public TreatmentMapperProfile()
        {
            CreateMap<TreatmentDto, TreatmentEntity>()
                .ForMember(dest => dest.F_Charges,
                    opt => opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.F_Charges)));
        }
    }
}
