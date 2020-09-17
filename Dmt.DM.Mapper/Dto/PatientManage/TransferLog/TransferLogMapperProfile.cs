using AutoMapper;
using Dmt.DM.Domain.Entity.PatientManage;

namespace Dmt.DM.Mapper.Dto.PatientManage.TransferLog
{
    public class TransferLogMapperProfile : Profile
    {
        public TransferLogMapperProfile()
        {
            CreateMap<TransferLogDto, TransferLogEntity>();
        }
    }
}
