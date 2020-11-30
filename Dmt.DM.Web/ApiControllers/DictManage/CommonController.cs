using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Dmt.DM.Web.ApiControllers.DictManage
{
    [Route("api/[controller]/[action]")]
    public class CommonController : BaseApiController
    {
        public IActionResult GetOrderFrequency()
        {
            var data = new List<FrequencyItem>
            {
                new FrequencyItem { id = "Qd", text = "每日一次" },
                new FrequencyItem { id = "Bid", text = "每日两次" },
                new FrequencyItem { id = "Tid", text = "每日三次" },
                new FrequencyItem { id = "Qid", text = "每日" },
                new FrequencyItem { id = "Qod", text = "隔日一次" },
                new FrequencyItem { id = "Qw", text = "每周一次" },
                new FrequencyItem { id = "Biw", text = "每周两次" },
                new FrequencyItem { id = "Tiw", text = "每周三次" },
                new FrequencyItem { id = "Qow", text = "隔周一次" },
                new FrequencyItem { id = "Q2w", text = "每两周一次" },
                new FrequencyItem { id = "Q3w", text = "每三周一次" },
                new FrequencyItem { id = "Q4w", text = "每四周一次" },
                new FrequencyItem { id = "Q1/2h", text = "30分钟一次" },
                new FrequencyItem { id = "Qh", text = "每小时一次" },
                new FrequencyItem { id = "Q2h", text = "二小时一次" },
                new FrequencyItem { id = "Q3h", text = "三小时一次" },
                new FrequencyItem { id = "Q4h", text = "四小时一次" },
                new FrequencyItem { id = "Q6h", text = "六小时一次" },
                new FrequencyItem { id = "Q8h", text = "八小时一次" },
                new FrequencyItem { id = "Q12h", text = "12小时一次" }
            };
            return Ok(data);
        }


        public IActionResult Test()
        {
            return BadRequest("测试错误！");
        }



        public IActionResult Test2()
        {
            throw new Exception("测试错误！");
        }
    }
    /// <summary>
    /// 用药频次数据结构
    /// </summary>
    public class FrequencyItem
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
