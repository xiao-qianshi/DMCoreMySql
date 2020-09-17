using System;
using System.Collections.Generic;
namespace Dmt.Dm.Domain.Dto.Orders
{
    public class OrderItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? operateTime { get; set; } = DateTime.Now;
    }

    public class ExecOrderInput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> items { get; set; }
    }
}