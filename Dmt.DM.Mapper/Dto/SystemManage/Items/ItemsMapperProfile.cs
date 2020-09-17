using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.Items
{
    public class ItemsMapperProfile: Profile
    {
        public ItemsMapperProfile()
        {
            CreateMap<ItemsDto, ItemsEntity>();
        }
    }
}
