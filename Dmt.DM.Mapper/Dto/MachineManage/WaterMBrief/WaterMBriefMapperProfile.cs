using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.WaterMBrief
{
    public class WaterMBriefMapperProfile : Profile
    {
        public WaterMBriefMapperProfile()
        {
            CreateMap<WaterMBriefDto, WaterMBriefEntity>()
                .ForMember(d=>d.F_RecordDate,
                    opt=>opt.PreCondition(s=>!string.IsNullOrWhiteSpace(s.F_RecordDate)))
                .ForMember(d => d.F_Value15,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value15)))
                .ForMember(d => d.F_Value16,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value16)))
                .ForMember(d => d.F_Value17,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value17)))
                .ForMember(d => d.F_Value18,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value18)))
                ;
        }
    }
}
