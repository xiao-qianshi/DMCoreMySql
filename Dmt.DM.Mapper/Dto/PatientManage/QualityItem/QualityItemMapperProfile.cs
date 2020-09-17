using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.QualityItem
{
    public class QualityItemMapperProfile : Profile
    {
        public QualityItemMapperProfile()
        {
            CreateMap<QualityItemDto, QualityItemEntity>()
                .ForMember(d => d.F_LowerCriticalValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_LowerCriticalValue)))
                .ForMember(d => d.F_LowerValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_LowerValue)))
                .ForMember(d => d.F_ResultType,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_ResultType)))
                .ForMember(d => d.F_UpperCriticalValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_UpperCriticalValue)))
                .ForMember(d => d.F_UpperValue,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_UpperValue)))
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)));

            CreateMap<PartitionDto, QualityItemPartitionEntity>()
                .ForMember(d => d.F_OrderNo, opt =>
                {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.OrderNo));
                    opt.MapFrom(s=>s.OrderNo);
                })
                .ForMember(d => d.F_LowerValue, opt =>
                {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.LowerValue));
                    opt.MapFrom(s => s.LowerValue);
                })
                .ForMember(d => d.F_LowerCheck, opt =>
                {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.LowerCheck));
                    opt.MapFrom(s => s.LowerCheck);
                })
                .ForMember(d => d.F_UpperValue, opt =>
                {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.UpperValue));
                    opt.MapFrom(s => s.UpperValue);
                })
                .ForMember(d => d.F_UpperCheck, opt =>
                {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.UpperCheck));
                    opt.MapFrom(s => s.UpperCheck);
                });
        }
    }
}
