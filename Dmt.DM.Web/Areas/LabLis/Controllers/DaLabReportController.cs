using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dmt.DM.Application;
using Dmt.DM.ExternalInterface.Lis;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dmt.DM.Web.Areas.LabLis.Controllers
{
    [Area("LabLis")]
    public class DaLabReportController : BaseController
    {
        private readonly IDaLabService _daLabService;
        private readonly IOptionsSnapshot<DASettings> _configuration;
        private readonly string _clientId;
        private readonly string _clientGuid;

        public DaLabReportController(IDaLabService daLabService, IOptionsSnapshot<DASettings> configuration)
        {
            _daLabService = daLabService;
            _configuration = configuration;
            _clientId = configuration.Value.UserId;
            _clientGuid = configuration.Value.Password;
        }

        [HttpPost]
        public async Task<IActionResult> FetchDaResult(DateTime? startDate, DateTime? endDate)
        {
            var res = await _daLabService.GetReport(_clientId, _clientGuid, startDate, endDate);
            res = "<root>" + res + "</root>";
            var document = XDocument.Parse(res);
            var rootElement = document.Root;
            var errorCode = rootElement.Descendants("Code").FirstOrDefault().Value;
            if (!errorCode.Equals("0")) return Error(rootElement.Descendants("Descript").FirstOrDefault().Value);
            var resultsDataSet = rootElement.Element("ResultsDataSet").Elements("Table");
            var sourceList = resultsDataSet.Select(r => new
            {
                Barcode = r.Element("BARCODE")?.Value?? "",
                SampleFrom = r.Element("SAMPLEFROM")?.Value ?? "",
                SampleType = r.Element("SAMPLETYPE")?.Value ?? "",
                CollectDate = r.Element("COLLECTDDATE")?.Value ?? "",
                SubmitDate =  r.Element("SUBMITDATE")?.Value ?? "",
                TestCode = r.Element("TESTCODE")?.Value ?? "",
                ApprDate = r.Element("APPRDATE")?.Value ?? "",
                Dept = r.Element("DEPT")?.Value ?? "",
                Servgrp = r.Element("SERVGRP")?.Value ?? "",
                UserNameId =  r.Element("USRNAMID")?.Value ?? "",
                UserName =  r.Element("USRNAM")?.Value ?? "",
                ApprvedById = r.Element("APPRVEDBYID")?.Value ?? "",
                ApprvedBy = r.Element("APPRVEDBY")?.Value ?? "",
                PatientName = r.Element("PATIENTNAME")?.Value ?? "",
                Gender =  r.Element("SEX")?.Value ?? "",
                Age =  r.Element("AGE")?.Value ?? "",
                AgeUnit =  r.Element("AGEUNIT")?.Value ?? "",
                Sinonym = r.Element("SINONYM")?.Value ?? "",
                ShortName =  r.Element("SHORTNAME")?.Value ?? "",
                Units =  r.Element("UNITS")?.Value ?? "",
                Final = r.Element("FINAL")?.Value ?? "",
                Analyte =  r.Element("ANALYTE")?.Value ?? "",
                DispLowHigh =  r.Element("DISPLOWHIGH")?.Value ?? "",
                S =  r.Element("S")?.Value ?? "",
                SynonimEn =  r.Element("SYNONIM_EN")?.Value ?? "",
                LowB =  r.Element("LOWB")?.Value ?? "",
                HighB =  r.Element("HIGHB")?.Value ?? "",
                Sorter =  r.Element("SORTER")?.Value ?? "",
                LongTxt =  r.Element("LONGTXT")?.Value ?? "",
                RangeFlag =  r.Element("RANGE_FLG")?.Value ?? "",
                SampleStatus =  r.Element("SAMPLESTATUS")?.Value ?? "",
                TestDate =  r.Element("TESTDATE")?.Value ?? "",
                Rn10 =  r.Element("RN10")?.Value ?? "",
                Rn20 =  r.Element("RN20")?.Value ?? ""
            });

            var data = sourceList.GroupBy(t => t.Barcode) //按照条码分组
                .Select(t => new
                {
                    //dx,
                    Barcode = t.Key,
                    t.FirstOrDefault().SampleFrom,
                    t.FirstOrDefault().PatientName,
                    t.FirstOrDefault().Gender,
                    t.FirstOrDefault().Age,
                    t.FirstOrDefault().AgeUnit,
                    t.FirstOrDefault().Dept,
                    Servgrps = t.GroupBy(s => s.Servgrp) //按照检验类别分组
                        .Select(s => new
                        {
                            Servgrp = s.Key,
                            Rows = s.GroupBy(r => r.TestCode) //按照检验大项代码分组
                                .Select(r => new
                                {
                                    TestCode = r.Key,
                                    r.FirstOrDefault().SampleType,
                                    r.FirstOrDefault().CollectDate,
                                    r.FirstOrDefault().SubmitDate,
                                    r.FirstOrDefault().ApprDate,
                                    r.FirstOrDefault().Servgrp,
                                    r.FirstOrDefault().UserNameId,
                                    r.FirstOrDefault().UserName,
                                    r.FirstOrDefault().ApprvedById,
                                    r.FirstOrDefault().ApprvedBy,
                                    r.FirstOrDefault().SampleStatus,
                                    r.FirstOrDefault().TestDate,
                                    Items = r.Select(i => new
                                    {
                                        i.Sinonym,
                                        i.ShortName,
                                        i.Units,
                                        i.Final,
                                        i.Analyte,
                                        i.DispLowHigh,
                                        i.S,
                                        i.SynonimEn,
                                        i.LowB,
                                        i.HighB,
                                        i.Sorter,
                                        i.LongTxt,
                                        i.RangeFlag,
                                        i.Rn10,
                                        i.Rn20
                                    }).OrderBy(i => i.Sorter)
                                }).OrderBy(r => r.TestCode)
                        }).GroupBy(s => s.Servgrp)

                }).OrderBy(t => t.Barcode);
            return Success("读取成功", data);
        }
    }
}
