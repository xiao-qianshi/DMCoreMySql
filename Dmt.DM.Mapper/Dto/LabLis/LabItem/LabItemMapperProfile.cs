using AutoMapper;
using Dmt.DM.Domain.Entity.LabLis;

namespace Dmt.DM.Mapper.Dto.LabLis.LabItem
{
    public class LabItemMapperProfile : Profile
    {
        public LabItemMapperProfile()
        {
            CreateMap<LabItemDto, LabItemEntity>()
                .ForMember(d => d.F_CuvetteNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_CuvetteNo)))
                .ForMember(d => d.F_IsExternal,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsExternal)))
                .ForMember(d => d.F_Sorter,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Sorter)))
                .ForMember(d => d.F_IsPeriodic,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsPeriodic)))
                .ForMember(d => d.F_TimeInterval,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_TimeInterval)))
                ;

        }
    }
}
