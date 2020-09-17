using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.ConclusionTemplate
{
    public class ConclusionTemplateMapperProfile: Profile
    {
        public ConclusionTemplateMapperProfile()
        {
            CreateMap<ConclusionTemplateDto, ConclusionTemplateEntity>()
                .ForMember(d => d.F_IsPrivate, 
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsPrivate)))
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)));
        }
    }
}
