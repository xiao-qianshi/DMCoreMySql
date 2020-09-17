using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.MachineProcess
{
    public class MachineProcessMapperProfile : Profile
    {
        public MachineProcessMapperProfile()
        {
            CreateMap<MachineProcessDto, MachineProcessEntity>()
                 .ForMember(d => d.F_DialylisNo,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialylisNo)))
                 .ForMember(d => d.F_VisitDate,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitDate)))
                 .ForMember(d => d.F_VisitNo,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitNo)))
                 .ForMember(d => d.F_ShowOrder,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ShowOrder)))
                 .ForMember(d => d.F_Option1,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option1)))
                 .ForMember(d => d.F_Option2,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option2)))
                 .ForMember(d => d.F_Option3,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option3)))
                 .ForMember(d => d.F_Option4,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option4)))
                 .ForMember(d => d.F_OperateTime,
                opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_OperateTime)))
                ;
        }

    }
}
