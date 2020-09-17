using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Patient
{
    public class PatientMapperProfile : Profile
    {
        public PatientMapperProfile()
        {
            CreateMap<PatientDto, PatientEntity>()
                //.ForMember(d=>d.F_Height, opt=> opt.Condition(s=>string.IsNullOrEmpty(s.F_Height)))
                .ForMember(dest => dest.F_Height, opt =>
                        opt.PreCondition(src=>!string.IsNullOrWhiteSpace(src.F_Height))
                        //opt.MapFrom(src=>src.F_IdealWeight)
                        /*  opt.Condition(src => (!string.IsNullOrEmpty(src.F_Height)))*/
                );
        }
    }
}
