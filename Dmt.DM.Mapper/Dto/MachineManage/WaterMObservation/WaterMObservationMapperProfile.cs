using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.WaterMObservation
{
    public class WaterMObservationMapperProfile : Profile
    {
        public WaterMObservationMapperProfile()
        {
            CreateMap<WaterMObservationDto, WaterMObservationEntity>()
                .ForMember(d => d.F_RecordDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RecordDate)))
                .ForMember(d => d.F_Value11,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value11)))
                .ForMember(d => d.F_Value14,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Value14)));
        }
    }
}
