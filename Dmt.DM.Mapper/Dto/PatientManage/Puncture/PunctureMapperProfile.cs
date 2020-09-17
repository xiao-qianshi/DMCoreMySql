using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Puncture
{
    public class PunctureMapperProfile : Profile
    {
        public PunctureMapperProfile()
        {
            CreateMap<PunctureDto, PunctureEntity>()
                .ForMember(d => d.F_EnabledMark,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_EnabledMark)))
                .ForMember(d => d.F_Distance1, 
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Distance1)))
                .ForMember(d => d.F_Distance2, 
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_Distance2)))
                .ForMember(d => d.F_OperateTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_OperateTime)))
                .ForMember(d => d.F_IsSuccess, 
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_IsSuccess)));
        }
    }
}
