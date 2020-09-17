using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.PatVisit
{
    public class PatVisitMapperProfile : Profile
    {
        public PatVisitMapperProfile()
        {
            CreateMap<PatVisitDto, PatVisitEntity>()
                .ForMember(d => d.F_DialysisNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialysisNo)))
                .ForMember(d => d.F_BirthDay,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_BirthDay)))
                .ForMember(d => d.F_VisitDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitDate)))
                .ForMember(d => d.F_VisitNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitNo)))
                .ForMember(d => d.F_WeightSXTH,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WeightSXTH)))
                .ForMember(d => d.F_FirstWeightTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_FirstWeightTime)))
                .ForMember(d => d.F_LastWeightValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_LastWeightValue)))
                .ForMember(d => d.F_WeightTQ,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WeightTQ)))
                .ForMember(d => d.F_WeightYT,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WeightYT)))
                .ForMember(d => d.F_WeightTH,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WeightTH)))
                .ForMember(d => d.F_WeightST,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_WeightST)))
                .ForMember(d => d.F_ExchangeAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ExchangeAmount)))
                .ForMember(d => d.F_ExchangeSpeed,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ExchangeSpeed)))
                .ForMember(d => d.F_DisinfectTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DisinfectTime)))
                .ForMember(d => d.F_EstimateHours,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EstimateHours)))
                .ForMember(d => d.F_DialysisStartTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialysisStartTime)))
                .ForMember(d => d.F_DialysisEndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialysisEndTime)))
                .ForMember(d => d.F_HeparinAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_HeparinAmount)))
                .ForMember(d => d.F_HeparinAddAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_HeparinAddAmount)))
                .ForMember(d => d.F_LowCa,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_LowCa)))
                .ForMember(d => d.F_Ca,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Ca)))
                .ForMember(d => d.F_K,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_K)))
                .ForMember(d => d.F_Na,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Na)))
                .ForMember(d => d.F_Hco3,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Hco3)))
                .ForMember(d => d.F_BloodSpeed,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_BloodSpeed)))
                .ForMember(d => d.F_DialysateTemperature,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialysateTemperature)))
                .ForMember(d => d.F_IsAcct,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsAcct)))
                .ForMember(d => d.F_IsCritical,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsCritical)))
                .ForMember(d => d.F_IsPrint,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsPrint)))
                .ForMember(d => d.F_MachineShowAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_MachineShowAmount)))
                .ForMember(d => d.F_RealExchangeAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RealExchangeAmount)))
                .ForMember(d => d.F_SystolicPressure,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_SystolicPressure)))
                .ForMember(d => d.F_DiastolicPressure,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DiastolicPressure)))
                .ForMember(d => d.F_Pulse,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Pulse)))
                .ForMember(d => d.F_Temperature,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Temperature)))
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)));
        }
    }
}
