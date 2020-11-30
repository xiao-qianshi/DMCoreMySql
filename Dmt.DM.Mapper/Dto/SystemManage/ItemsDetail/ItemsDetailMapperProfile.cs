using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dmt.DM.Domain.Entity.SystemManage;

namespace Dmt.DM.Mapper.Dto.SystemManage.ItemsDetail
{
    public class ItemsDetailMapperProfile : Profile
    {
        public ItemsDetailMapperProfile()
        {
            CreateMap<ItemsDetailDto, ItemsDetailEntity>();
        }
    }
}
