using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.DialysisObservation
{
    public class DialysisObservationMapperProfile : Profile
    {
        public DialysisObservationMapperProfile()
        {
            CreateMap<DialysisObservationDto, DialysisObservationEntity>()
                .ForMember(d => d.F_NurseOperatorTime,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_NurseOperatorTime)))
                .ForMember(d => d.F_A,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_A)))
                .ForMember(d => d.F_BF,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_BF)))
                .ForMember(d => d.F_C,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_C)))
                .ForMember(d => d.F_GSL,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_GSL)))
                .ForMember(d => d.F_HR,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_HR)))
                .ForMember(d => d.F_SSY,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_SSY)))
                .ForMember(d => d.F_SZY,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_SZY)))
                .ForMember(d => d.F_T,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_T)))
                .ForMember(d => d.F_TMP,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_TMP)))
                .ForMember(d => d.F_UFV,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_UFV)))
                .ForMember(d => d.F_UFR,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_UFR)))
                .ForMember(d => d.F_V,
                    opt => opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.F_V)));

            CreateMap<SaveDataInput, DialysisObservationEntity>()
                .ForMember(d => d.F_NurseOperatorTime,
                    opt =>
                    {
                        opt.PreCondition(s=>!string.IsNullOrWhiteSpace(s.Ob_NurseOperatorTime));
                        opt.MapFrom(s => s.Ob_NurseOperatorTime);
                    }

                )
            .ForMember(d => d.F_A,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_A));
                    opt.MapFrom(s => s.Ob_A);
                })
            .ForMember(d => d.F_BF,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_BF));
                    opt.MapFrom(s => s.Ob_BF);
                })
            .ForMember(d => d.F_C,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_C));
                    opt.MapFrom(s => s.Ob_C);
                })
            .ForMember(d => d.F_GSL,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_GSL));
                    opt.MapFrom(s => s.Ob_GSL);
                })
            .ForMember(d => d.F_HR,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_HR));
                    opt.MapFrom(s => s.Ob_HR);
                })
            .ForMember(d => d.F_SSY,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_SSY));
                    opt.MapFrom(s => s.Ob_SSY);
                })
            .ForMember(d => d.F_SZY,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_SZY));
                    opt.MapFrom(s => s.Ob_SZY);
                })
            .ForMember(d => d.F_T,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_T));
                    opt.MapFrom(s => s.Ob_T);
                })
            .ForMember(d => d.F_TMP,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_TMP));
                    opt.MapFrom(s => s.Ob_TMP);
                })
            .ForMember(d => d.F_UFV,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_UFV));
                    opt.MapFrom(s => s.Ob_UFV);
                })
            .ForMember(d => d.F_UFR,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_UFR));
                    opt.MapFrom(s => s.Ob_UFR);
                })
            .ForMember(d => d.F_V,
                opt => {
                    opt.PreCondition(s => !string.IsNullOrWhiteSpace(s.Ob_V));
                    opt.MapFrom(s => s.Ob_V);
                });
        }
    }
}
