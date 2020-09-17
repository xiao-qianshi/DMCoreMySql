using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Setting
{
    public class SettingMapperProfile : Profile
    {
        public SettingMapperProfile()
        {
            CreateMap<SettingDto, SettingEntity>()
                .ForMember(d => d.F_ExchangeAmount,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ExchangeAmount)))
                .ForMember(d => d.F_ExchangeSpeed,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ExchangeSpeed)))
                .ForMember(d => d.F_EstimateHours,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EstimateHours)))
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
                ;
        }
    }
}
