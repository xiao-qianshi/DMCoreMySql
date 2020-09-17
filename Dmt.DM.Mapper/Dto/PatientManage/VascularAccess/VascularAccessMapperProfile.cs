using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.VascularAccess
{
    public class VascularAccessMapperProfile : Profile
    {
        public VascularAccessMapperProfile()
        {
            CreateMap<VascularAccessDto, VascularAccessEntity>()
                .ForMember(d => d.F_OperateTime,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_OperateTime)))
                .ForMember(d => d.F_BloodSpeed_Idea,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_BloodSpeed_Idea)))
                .ForMember(d => d.F_BloodSpeed,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_BloodSpeed)))
                .ForMember(d => d.F_FirstUseTime,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_FirstUseTime)))
                .ForMember(d => d.F_DiscardTime,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_DiscardTime)))
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrEmpty(s.F_EnabledMark)))
                ;
        }
    }
}
