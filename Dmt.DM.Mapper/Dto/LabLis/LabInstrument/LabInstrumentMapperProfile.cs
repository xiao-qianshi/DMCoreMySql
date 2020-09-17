using AutoMapper;
using Dmt.DM.Domain.Entity.LabLis;

namespace Dmt.DM.Mapper.Dto.LabLis.LabInstrument
{
    public class LabInstrumentMapperProfile : Profile
    {
        public LabInstrumentMapperProfile()
        {
            CreateMap<LabInstrumentDto, LabInstrumentEntity>()
                .ForMember(d => d.F_IsDuplex,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsDuplex)))
                .ForMember(d => d.F_IsExternal,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsExternal)))
                .ForMember(d => d.F_Sorter,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Sorter)))
                .ForMember(d => d.F_WorkStationPort,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WorkStationPort)))
                .ForMember(d => d.F_IsRegistered,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsRegistered)))
                ;

            CreateMap<LabInstrumentItemDto, LabInstrumentItemEntity>()
                .ForMember(d => d.F_ResultType,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ResultType)))
                .ForMember(d => d.F_Sorter,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Sorter)))
                .ForMember(d => d.F_KeepDecimal,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_KeepDecimal)))
                .ForMember(d => d.F_DefaultValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DefaultValue)))
                .ForMember(d => d.F_ReferenceMinValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ReferenceMinValue)))
                .ForMember(d => d.F_ReferenceMaxValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ReferenceMaxValue)))
                .ForMember(d => d.F_CriticalMinValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_CriticalMinValue)))
                .ForMember(d => d.F_CriticalMaxValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_CriticalMaxValue)))
                .ForMember(d => d.F_IsQualityItem,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsQualityItem)))
                .ForMember(d => d.F_ConvertCoefficient,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ConvertCoefficient)))
                .ForMember(d => d.IsHiddenItem,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.IsHiddenItem)))
                ;


        }
    }
}
