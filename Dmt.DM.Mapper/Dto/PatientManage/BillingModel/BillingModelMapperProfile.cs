using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.BillingModel
{
    public class BillingModelMapperProfile : Profile
    {
        public BillingModelMapperProfile()
        {
            CreateMap<BillingModelDto, BillingModelEntity>()
                .ForMember(dest => dest.F_Amount,
                    opt => opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.F_Amount)))
                .ForMember(dest => dest.F_Charges,
                    opt => opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.F_Charges)));
        }
    }
}
