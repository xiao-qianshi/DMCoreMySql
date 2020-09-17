using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dmt.DM.Domain.Entity.MachineManage;

namespace Dmt.DM.Mapper.Dto.MachineManage.WaterMDisinfect
{
    public class WaterMDisinfectMapperProfile : Profile
    {
        public WaterMDisinfectMapperProfile()
        {
            CreateMap<WaterMDisinfectDto, WaterMDisinfectEntity>()
                .ForMember(d => d.F_DisinfectDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DisinfectDate)))
                .ForMember(d => d.F_DisinfectantVolume,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DisinfectantVolume)))
                .ForMember(d => d.F_Option1,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option1)))
                .ForMember(d => d.F_RecyclingStartTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RecyclingStartTime)))
                .ForMember(d => d.F_RecyclingEndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RecyclingEndTime)))
                .ForMember(d => d.F_RecyclingMinutes,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RecyclingMinutes)))
                .ForMember(d => d.F_SoakEndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_SoakEndTime)))
                .ForMember(d => d.F_SoakMinutes,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_SoakMinutes)))
                .ForMember(d => d.F_RinseStartTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RinseStartTime)))
                .ForMember(d => d.F_RinseEndTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RinseEndTime)))
                .ForMember(d => d.F_RinseMinutes,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_RinseMinutes)))
                .ForMember(d => d.F_Option2,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option2)))
                .ForMember(d => d.F_Option3,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Option3)))
                ;
        }
    }
}
