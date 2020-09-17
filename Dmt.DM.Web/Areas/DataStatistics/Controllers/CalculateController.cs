using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Code;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dmt.DM.Web.Areas.DataStatistics.Controllers
{
    [Area("DataStatistics")]
    [AllowAnonymous]
    public class CalculateController : BaseController
    {
        public IActionResult GetCalcJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            //mmol/L
            var preUrea = json.Value<double>("preUrea");
            //mmol/L
            var postUrea = json.Value<double>("postUrea");
            //hour
            var duration = json.Value<double>("duration");
            //hour
            var frequency = json.Value<double>("frequency");
            //L
            var ultrafiltrationVolume = json.Value<double>("preWeight") - json.Value<double>("postWeight");
            //Kg
            var postWeight = json.Value<double>("postWeight");
            //通路类型 1 内瘘  2导管
            var vascularAccess = json.Value<string>("vascularAccess");

            //spKtV/V
            var result1 = -Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * ultrafiltrationVolume / postWeight;
            //eKt/V
            var result2 = vascularAccess.Equals("2") ?
                result1 - (0.47 * result1 / duration) + 0.02
                : result1 - (0.6 * result1 / duration) + 0.03;
            //stdKt/V
            var result3 = (10080 * (1 - Math.Exp(-result2))) / duration  //分子
                /
                (
                    (1 - Math.Exp(-result2)) / result1 + 10080 / (duration * frequency) - 1
                )
                ;
            var result4 = 100 * (1 - postUrea / preUrea);

            var data = new
            {
                value1 = result1.ToDouble(2),
                value2 = result2.ToDouble(2),
                value3 = result3.ToDouble(2),
                value4 = result4.ToDouble(2),
            };
            return Content(data.ToJson());
        }

        public IActionResult GetKtvJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            //mmol/L
            var preUrea = json.Value<double>("preUrea");
            //mmol/L
            var postUrea = json.Value<double>("postUrea");
            //hour
            var duration = json.Value<double>("duration");
            //L
            var ultrafiltrationVolume = json.Value<double>("preWeight") - json.Value<double>("postWeight");
            //Kg
            var postWeight = json.Value<double>("postWeight");
            //spKtV/V
            var result1 = -Math.Log(postUrea / preUrea - 0.008 * duration) + (4 - 3.5 * postUrea / preUrea) * ultrafiltrationVolume / postWeight;

            var data = new
            {
                value1 = result1.ToDouble(2),
            };
            return Content(data.ToJson());
        }
    }
}
