using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.ProcessFlow
{
    public class ProcessFlowMapperProfile : Profile
    {
        public ProcessFlowMapperProfile()
        {
            CreateMap<ProcessFlowDto, ProcessFlowEntity>()
                .ForMember(d => d.F_DialylisNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_DialylisNo)))
                .ForMember(d => d.F_VisitDate,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitDate)))
                .ForMember(d => d.F_VisitNo,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_VisitNo)))
                .ForMember(d => d.F_TotalHours,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_TotalHours)))
                .ForMember(d => d.F_PreUrea,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_PreUrea)))
                .ForMember(d => d.F_PostUrea,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_PostUrea)))
                .ForMember(d => d.F_PreWeight,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_PreWeight)))
                .ForMember(d => d.F_PostWeight,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_PostWeight)))
                .ForMember(d => d.F_Result,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Result)))
                ;
        }
    }
}
