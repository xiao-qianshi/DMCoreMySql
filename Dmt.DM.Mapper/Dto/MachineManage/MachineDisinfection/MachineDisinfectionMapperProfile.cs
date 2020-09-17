using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.MachineDisinfection
{
    public class MachineDisinfectionMapperProfile : Profile
    {
        public MachineDisinfectionMapperProfile()
        {
            CreateMap<MachineDisinfectionDto, MachineDisinfectionEntity>()
                .ForMember(d => d.F_VisitDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitDate)))
                .ForMember(d => d.F_VisitNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitNo)))
                .ForMember(d => d.F_ShowOrder,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ShowOrder)))
                .ForMember(d => d.F_StartTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_StartTime)))
                .ForMember(d => d.F_EndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EndTime)))
                .ForMember(d => d.F_WipeStartTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WipeStartTime)))
                .ForMember(d => d.F_WipeEndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WipeEndTime)))
                .ForMember(d => d.F_Option1,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option1)))
                .ForMember(d => d.F_Option2,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option2)))
                .ForMember(d => d.F_Option3,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option3)))
                .ForMember(d => d.F_Option4,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option4)))
                .ForMember(d => d.F_Option5,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option5)))
                .ForMember(d => d.F_Option6,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option6)))
                ;
        }
    }
}
