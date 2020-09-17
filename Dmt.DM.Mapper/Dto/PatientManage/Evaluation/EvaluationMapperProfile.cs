using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Evaluation
{
    public class EvaluationMapperProfile : Profile
    {
        public EvaluationMapperProfile()
        {
            CreateMap<EvaluationDto, EvaluationEntity>()
                .ForMember(d => d.F_VisitDate, opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitDate)))
                .ForMember(d => d.Sctxdate, opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Sctxdate)));
        }
    }
}
