using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Infection
{
    public class InfectionMapperProfile: Profile
    {
        public InfectionMapperProfile()
        {
            CreateMap<InfectionDto, InfectionEntity>()
                .ForMember(d => d.F_ReportDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ReportDate)))
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)))
                .ForMember(d => d.F_Item1,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item1)))
                .ForMember(d => d.F_Item2,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item2)))
                .ForMember(d => d.F_Item3,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item3)))
                .ForMember(d => d.F_Item4,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item4)))
                .ForMember(d => d.F_Item5,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item5)))
                .ForMember(d => d.F_Item6,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item6)))
                .ForMember(d => d.F_Item7,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Item7)));

        }
    }
}
