using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.MedicalRecord
{
    public class MedicalRecordMapperProfile: Profile
    {
        public MedicalRecordMapperProfile()
        {
            CreateMap<MedicalRecordDto, MedicalRecordEntity>()
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)))
                .ForMember(d => d.F_AuditFlag,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_AuditFlag)))
                .ForMember(d => d.F_AuditTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_AuditTime)))
                .ForMember(d => d.F_CreatorTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ContentTime)));
        }
    }
}
