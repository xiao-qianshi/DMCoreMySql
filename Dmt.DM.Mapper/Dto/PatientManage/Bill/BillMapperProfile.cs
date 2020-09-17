using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.Bill
{
    public class BillMapperProfile : Profile
    {
        public BillMapperProfile()
        {
            CreateMap<BillDto, BillingEntity>()
                .ForMember(d => d.F_Amount, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_Amount)))
                .ForMember(d => d.F_BillingDateTime, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_BillingDateTime)))
                .ForMember(d => d.F_Charges, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_Charges)))
                .ForMember(d => d.F_Costs, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_Costs)))
                .ForMember(d => d.F_DialylisNo, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_DialylisNo)))
                .ForMember(d => d.F_IsAcct, opt => opt.Condition(s => string.IsNullOrWhiteSpace(s.F_IsAcct)));
        }
    }
}
