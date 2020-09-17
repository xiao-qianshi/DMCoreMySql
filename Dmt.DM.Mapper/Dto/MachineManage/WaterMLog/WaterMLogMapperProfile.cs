using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.WaterMLog
{
    public class WaterMLogMapperProfile : Profile
    {
        public WaterMLogMapperProfile()
        {
            CreateMap<WaterMLogDto, WaterMLogEntity>()
                .ForMember(d => d.F_LogDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_LogDate)))
                .ForMember(d => d.F_Value1,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value1)))
                .ForMember(d => d.F_Value2,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value2)))
                .ForMember(d => d.F_Value3,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value3)))
                .ForMember(d => d.F_Value4,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value4)))
                .ForMember(d => d.F_Value5,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value5)))
                .ForMember(d => d.F_Value6,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value6)))
                .ForMember(d => d.F_Value7,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value7)))
                .ForMember(d => d.F_Value8,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value8)))
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
