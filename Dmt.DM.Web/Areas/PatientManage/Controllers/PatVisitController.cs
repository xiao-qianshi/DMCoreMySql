using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Domain.Entity.ReportPrint;
using Dmt.DM.Domain.Entity.ReportPrint.DialysisRecord;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.PatVisit;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class PatVisitController : BaseController
    {
        private readonly IPatVisitApp _patVisitApp;
        private readonly IPatientApp _patientApp;
        private readonly IOrdersApp _ordersApp;
        private readonly IOrdersExecLogApp _ordersExecLogApp;
        private readonly IMaterialApp _materialApp;
        private readonly IDrugsApp _drugsApp;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;
        private readonly IBillingModelApp _billingModelApp;
        private readonly IBillingApp _billingApp;
        private readonly IStorageApp _storageApp;
        private readonly ITreatmentApp _treatmentApp;
        private readonly IOrganizeApp _organizeApp;
        private readonly IDialysisObservationApp _dialysisObservationApp;

        public PatVisitController(
            IPatVisitApp patVisitApp,
            IPatientApp patientApp,
            IOrdersApp ordersApp,
            IOrdersExecLogApp ordersExecLogApp,
            IMaterialApp materialApp,
            IDrugsApp drugsApp,
            IMapper mapper,
            IUsersService usersService,
            IBillingModelApp billingModelApp,
            IBillingApp billingApp,
            IStorageApp storageApp,
            ITreatmentApp treatmentApp,
            IOrganizeApp organizeApp,
            IDialysisObservationApp dialysisObservationApp
        )
        {
            _patVisitApp = patVisitApp;
            _patientApp = patientApp;
            _ordersApp = ordersApp;
            _ordersExecLogApp = ordersExecLogApp;
            _materialApp = materialApp;
            _drugsApp = drugsApp;
            _mapper = mapper;
            _usersService = usersService;
            _billingModelApp = billingModelApp;
            _billingApp = billingApp;
            _storageApp = storageApp;
            _treatmentApp = treatmentApp;
            _organizeApp = organizeApp;
            _dialysisObservationApp = dialysisObservationApp;
        }

        public async Task<IActionResult> GetGridJson(Pagination pagination, string keyword, int visitNo, int status, DateTime? startDate, DateTime? endDate)
        {
            var dict = new Dictionary<string, string>();
            var rows = await _patVisitApp.GetList(pagination, keyword, visitNo, status, startDate, endDate);
            foreach (var item in rows)
            {
                var id = item.F_DialyzerType1;
                if (id != null)
                {
                    if (!dict.ContainsKey(id))
                    {
                        var materialEntity = await _materialApp.GetForm(id);
                        dict.Add(id, materialEntity.F_MaterialName);
                    }
                    item.F_DialyzerType1 = dict[id];
                }
                id = item.F_DialyzerType2;
                if (id != null)
                {
                    if (!dict.ContainsKey(id))
                    {
                        var materialEntity = await _materialApp.GetForm(id);
                        dict.Add(id, materialEntity.F_MaterialName);
                    }
                    item.F_DialyzerType2 = dict[id];
                }
                id = item.F_HeparinType;
                if (id != null)
                {
                    if (!dict.ContainsKey(id))
                    {
                        var drugsEntity = await _drugsApp.GetForm(id);
                        dict.Add(id, drugsEntity.F_DrugName);
                    }
                    item.F_HeparinType = dict[id];
                }
            }

            var data = new
            {
                rows,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());

        }
     
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _patVisitApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<PatVisitDto> input)
        {
            if (!string.IsNullOrWhiteSpace(input.KeyValue))
            {
                if (_patVisitApp.GetArchiveStatus(input.KeyValue))
                {
                    return Error("已归档。");
                }
            }

            PatVisitEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<PatVisitEntity>(input.Entity);
            }
            else
            {
                entity = await _patVisitApp.GetForm(input.KeyValue);
            }

            await _patVisitApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 计费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitCharge([FromBody]BaseInput input)
        {
            if (_patVisitApp.GetAcctStatus(input.KeyValue))
            {
                return Error("已计费");
            }
            var entity = await _patVisitApp.GetForm(input.KeyValue);
            var patient = await _patientApp.GetForm(entity.F_Pid);
            var user = await _usersService.GetCurrentUserAsync();
            //var billApp = new BillingApp();
            //var storageApp = new StorageApp();

            var date = DateTime.Now;
            //费用模板计费
            //BillingModelApp billingModel = new BillingModelApp();
            var model = (from r in await _billingModelApp.GetList()
                where r.F_DialysisType == entity.F_DialysisType && r.F_VascularAccess == entity.F_VascularAccess
                select new
                {
                    r.F_Amount,
                    r.F_Charges,
                    r.F_ItemClass,
                    r.F_ItemCode,
                    r.F_ItemId,
                    r.F_ItemName,
                    r.F_ItemSpec,
                    r.F_ItemUnit,
                    r.F_Supplier
                }).ToList();
            if (model.Count > 0)
            {
                var drugs = (from r in model
                    where r.F_ItemClass == "药品"
                    select r).ToList();
                if (drugs.Count > 0)
                {
                    //DrugsApp drugsApp = new DrugsApp();
                    foreach (var item in drugs)
                    {
                        int count = item.F_Amount.ToInt();
                        if (count <= 0) continue;
                        var dict = await _drugsApp.GetForm(item.F_ItemId);
                        //计费
                        await _billingApp.SubmitForm(new BillingEntity
                        {
                            F_Amount = count,
                            F_BillingDateTime = date,
                            F_BillingPerson = user.F_RealName,
                            F_BillingPersonId = user.F_Id,
                            F_Charges = dict.F_Charges,
                            F_Costs = (dict.F_Charges * count).ToFloat(2),
                            F_DialylisNo = patient.F_DialysisNo,
                            F_Pid = patient.F_Id,
                            F_PGender = patient.F_Gender,
                            F_PName = patient.F_Name,
                            F_ItemClass = "药品",
                            F_ItemCode = dict.F_DrugCode,
                            F_ItemId = dict.F_Id,
                            F_ItemName = dict.F_DrugName,
                            F_ItemSpec = dict.F_DrugSpec,
                            F_ItemSpell = dict.F_DrugSpell,
                            F_ItemUnit = dict.F_DrugUnit,
                            F_Supplier = dict.F_DrugSupplier,
                            F_EnabledMark = true,
                            F_IsAcct = false
                        }, new object());
                        //减库存
                        var storage = await _storageApp.GetFormByItemId(item.F_ItemId);
                        if (storage != null)
                        {
                            storage.F_Amount -= count;
                            await _storageApp.UpdateForm(storage);
                        }
                    }
                }

                var materials = (from r in model
                    where r.F_ItemClass == "耗材"
                    select r).ToList();
                if (materials.Count > 0)
                {
                    //MaterialApp materialApp = new MaterialApp();
                    foreach (var item in materials)
                    {
                        int count = item.F_Amount.ToInt();
                        if (count <= 0) continue;
                        var dict = await _materialApp.GetForm(item.F_ItemId);
                        //计费
                        await _billingApp.SubmitForm(new BillingEntity
                        {
                            F_Amount = count,
                            F_BillingDateTime = date,
                            F_BillingPerson = user.F_RealName,
                            F_BillingPersonId = user.F_Id,
                            F_Charges = dict.F_Charges,
                            F_Costs = (dict.F_Charges * count).ToFloat(2),
                            F_DialylisNo = patient.F_DialysisNo,
                            F_Pid = patient.F_Id,
                            F_PGender = patient.F_Gender,
                            F_PName = patient.F_Name,
                            F_ItemClass = "耗材",
                            F_ItemCode = dict.F_MaterialCode,
                            F_ItemId = dict.F_Id,
                            F_ItemName = dict.F_MaterialName,
                            F_ItemSpec = dict.F_MaterialSpec,
                            F_ItemSpell = dict.F_MaterialSpell,
                            F_ItemUnit = dict.F_MaterialUnit,
                            F_Supplier = dict.F_MaterialSupplier,
                            F_EnabledMark = true,
                            F_IsAcct = false
                        }, new object());
                        //减库存
                        var storage = await _storageApp.GetFormByItemId(item.F_ItemId);
                        if (storage != null)
                        {
                            storage.F_Amount -= count;
                            await _storageApp.UpdateForm(storage);
                        }
                    }
                }

                var treatments = (from r in model
                    where r.F_ItemClass == "诊疗"
                    select r).ToList();
                if (treatments.Count > 0)
                {
                    //TreatmentApp treatmentApp = new TreatmentApp();
                    foreach (var item in treatments)
                    {
                        var amount = item.F_Amount.ToFloat(2);
                        var dict = await _treatmentApp.GetForm(item.F_ItemId);
                        //计费
                        await _billingApp.SubmitForm(new BillingEntity
                        {
                            F_Amount = amount,
                            F_BillingDateTime = date,
                            F_BillingPerson = user.F_RealName,
                            F_BillingPersonId = user.F_Id,
                            F_Charges = dict.F_Charges,
                            F_Costs = (dict.F_Charges * amount).ToFloat(2),
                            F_DialylisNo = patient.F_DialysisNo,
                            F_Pid = patient.F_Id,
                            F_PGender = patient.F_Gender,
                            F_PName = patient.F_Name,
                            F_ItemClass = "诊疗",
                            F_ItemCode = dict.F_TreatmentCode,
                            F_ItemId = dict.F_Id,
                            F_ItemName = dict.F_TreatmentName,
                            F_ItemSpec = dict.F_TreatmentSpec,
                            F_ItemSpell = dict.F_TreatmentSpell,
                            F_ItemUnit = dict.F_TreatmentUnit,
                            F_EnabledMark = true,
                            F_IsAcct = false
                        }, new object());
                    }
                }
            }
            //抗凝剂计费
            var drugId = entity.F_HeparinType;
            if (drugId != null)
            {
                var dict = await _drugsApp.GetForm(drugId);
                var amount = entity.F_HeparinAmount.ToFloat(4);
                int count = 0;
                if (amount > 0)
                {
                    int fz = (amount * 10000).ToInt();
                    int fm = (dict.F_DrugMiniAmount * 10000).ToInt();
                    count = fz % fm == 0 ? fz / fm : fz / fm + 1; //非整数倍  数量追加1 
                }
                if (count > 0)
                {
                    //计费
                    await _billingApp.SubmitForm(new BillingEntity
                    {
                        F_Amount = count,
                        F_BillingDateTime = date,
                        F_BillingPerson = user.F_RealName,
                        F_BillingPersonId = user.F_Id,
                        F_Charges = dict.F_Charges,
                        F_Costs = (dict.F_Charges * count).ToFloat(2),
                        F_DialylisNo = patient.F_DialysisNo,
                        F_Pid = patient.F_Id,
                        F_PGender = patient.F_Gender,
                        F_PName = patient.F_Name,
                        F_ItemClass = "药品",
                        F_ItemCode = dict.F_DrugCode,
                        F_ItemId = dict.F_Id,
                        F_ItemName = dict.F_DrugName,
                        F_ItemSpec = dict.F_DrugSpec,
                        F_ItemSpell = dict.F_DrugSpell,
                        F_ItemUnit = dict.F_DrugUnit,
                        F_Supplier = dict.F_DrugSupplier,
                        F_EnabledMark = true,
                        F_IsAcct = false
                    }, new object());
                    //减库存
                    var storage = await _storageApp.GetFormByItemId(drugId);
                    if (storage != null)
                    {
                        storage.F_Amount -= count;
                        await _storageApp.UpdateForm(storage);
                    }
                }
            }

            entity.F_IsAcct = true;
            await _patVisitApp.UpdateForm(entity);
            return Success("计费成功。");
        }
        /// <summary>
        /// 保存透析小结
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CommitMemo([FromBody]CommitMemoInput input)
        {
            if (_patVisitApp.GetArchiveStatus(input.Id))
            {
                return Error("已归档。");
            }

            await _patVisitApp.SaveMemo(input.Id, input.Content);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            if (_patVisitApp.GetArchiveStatus(input.KeyValue))
            {
                return Error("已归档。");
            }
            await _patVisitApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveForm([FromBody]BaseInput input)
        {
            var entity = await _patVisitApp.GetForm(input.KeyValue);
            if (entity.F_IsArchive == true)
            {
                return Error("已归档。");
            }
            if (entity.F_DialysisStartTime == null)
            {
                return Error("未开始上机。");
            }
            if (entity.F_DialysisEndTime == null)
            {
                return Error("未下机。");
            }
            if (entity.F_Memo == null)
            {
                return Error("未填写治疗小节。");
            }
            entity.F_IsArchive = true;
            await _patVisitApp.UpdateForm(entity);
            return Success("锁定成功。");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Filter()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CheckFilter()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Prepare()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PrintBatch()
        {
            return View();
        }


        public IActionResult GetPrepareRecordJson(DateTime visitDate,int? visitNo)
        {
            var query = _patVisitApp.GetList(visitDate,visitNo.ToInt());

            var data = query.Select(r => new
            {
                r.F_Id,
                r.F_Name,
                r.F_DialysisType
            }).OrderBy(r => r.F_Name).ToList();
            return Content(data.ToJson());
        }

        /// <summary>
        /// 根据患者ID查询记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetHistoryRecordsJson(string keyword)
        {
            var list = _patVisitApp.GetList().Where(t => t.F_Pid == keyword).Select(r => new
            {
                r.F_Id,
                r.F_VisitDate,
                r.F_DialysisType
            }).OrderByDescending(r=>r.F_VisitDate).ToList();

            var data = list.Select(t => new
            {
                id = t.F_Id,
                text = t.F_VisitDate.ToDateString() + t.F_DialysisType
            });
            return Content(data.ToJson());
        }

        public IActionResult GetPrintRecordIDsJson(string keyword)
        {
            var json = keyword.ToJObject();
            var patientId = json.Value<string>("patientId");
            var query = _patVisitApp.GetList().Where(t => t.F_Pid == patientId);
            if (DateTime.TryParse(json.Value<string>("startDate")??"",out var date))
            {
                var result = date;
                query = query.Where(t => t.F_VisitDate >= result);
            }
            if (DateTime.TryParse(json.Value<string>("endDate") ?? "", out date))
            {
                var result = date;
                query = query.Where(t => t.F_VisitDate <= result);
            }
            var data = query.Where(t=>t.F_DialysisEndTime!=null &&t.F_DialysisStartTime!=null).
                       Select(r=> new
                       {
                           r.F_Id,
                           r.F_Name,
                           r.F_DialysisType,
                           F_VisitDate = r.F_VisitDate.ToDateString()
                       });
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetPreparePreViewJson(DateTime visitDate, int visitNo, string groupName = "")
        {
            if (string.IsNullOrEmpty(groupName)) groupName = "";
            var dialysisPrepare = new DialysisPrepare
            {
                PrepareRecords = new List<PrepareRecord>(),
                Summations = new List<Summation>(),
                HospialName = await _organizeApp.GetHospitalName(),
                HospialLogo = await _organizeApp.GetHospitalLogo(),
            };

            var drugDict = new Dictionary<string, string>();
            var materialDict = new Dictionary<string, string>();
  
            //增加分组判断
            var list = _patVisitApp.GetList(visitDate, visitNo).Where(t => groupName == "" || t.F_GroupName==groupName)
                .OrderBy(t => t.F_GroupName).ThenBy(t => t.F_DialysisBedNo.ToInt())
                .Select(r => new
                {
                    VisitDate = r.F_VisitDate.ToChineseDateString(),
                    VisitNo = r.F_VisitNo.ToString(), //班次
                    Name = r.F_Name,
                    DialysisNo = r.F_DialysisNo.ToString(),
                    GroupName = r.F_GroupName,
                    DialysisBedNo = r.F_DialysisBedNo,
                    DialysisType = r.F_DialysisType,
                    DialyzerType1 = r.F_DialyzerType1,
                    DialyzerType2 = r.F_DialyzerType2,
                    VascularAccess = r.F_VascularAccess,
                    AccessName = r.F_AccessName,
                    HeparinType = r.F_HeparinType,
                    HeparinAmount = r.F_HeparinAmount,
                    HeparinUnit = r.F_HeparinUnit,
                    HeparinAddAmount = r.F_HeparinAddAmount,
                    HeparinAddSpeedUnit = r.F_HeparinAddSpeedUnit
                });

            foreach (var item in list)
            {
                var prepareRecord = dialysisPrepare.PrepareRecords.Find(t => t.VisitNo==item.VisitNo);
                if (prepareRecord == null)
                {
                    prepareRecord = new PrepareRecord
                    {
                        VisitDate = item.VisitDate,
                        VisitNo = item.VisitNo,
                        PrepareRecordDetails = new List<PrepareRecordDetail>(),
                        PrepareSumDetails = new List<PrepareSumDetail>()
                    };
                    dialysisPrepare.PrepareRecords.Add(prepareRecord);
                }
                var key = string.Empty;
                if (item.HeparinType != null)
                {
                    key = item.HeparinType;
                    if (!drugDict.ContainsKey(key))
                    {
                        drugDict[key] = (await _drugsApp.GetForm(key))?.F_DrugName ?? "";
                    }

                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName == drugDict[key]);
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "抗凝剂",
                            ItemName = drugDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName == drugDict[key]);
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "抗凝剂",
                            ItemName = drugDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }
                if (item.DialyzerType1 != null)
                {
                    key = item.DialyzerType1;
                    if (!materialDict.ContainsKey(key))
                    {
                        materialDict[key] = (await _materialApp.GetForm(key))?.F_MaterialName ?? "";
                    }

                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName == materialDict[key]);
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName == materialDict[key]);
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }
                if (item.DialyzerType2 != null)
                {
                    key = item.DialyzerType2;
                    if (!materialDict.ContainsKey(key))
                    {
                        materialDict[key] = (await _materialApp.GetForm(key))?.F_MaterialName ?? "";
                    }

                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName == materialDict[key]);
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName == materialDict[key]);
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }

                prepareRecord.PrepareRecordDetails.Add(new PrepareRecordDetail
                {
                    Name = item.Name,
                    DialysisNo = item.DialysisNo,
                    GroupName = item.GroupName,
                    DialysisBedNo = item.DialysisBedNo,
                    DialysisType = item.DialysisType,
                    DialyzerType1 = item.DialyzerType1 == null ? "" : materialDict[item.DialyzerType1],
                    DialyzerType2 = item.DialyzerType2 == null ? "" : materialDict[item.DialyzerType2],
                    VascularAccess = item.VascularAccess,
                    AccessName = item.AccessName,
                    HeparinType = item.HeparinType == null ? "" : drugDict[item.HeparinType],
                    HeparinAmount = item.HeparinAmount.ToString(),
                    HeparinUnit = item.HeparinUnit,
                    HeparinAddAmount = item.HeparinAddAmount.ToString(),
                    HeparinAddSpeedUnit = item.HeparinAddSpeedUnit
                });
            }
            //排序
            foreach (var item in dialysisPrepare.PrepareRecords)
            {
                item.PrepareRecordDetails = item.PrepareRecordDetails/*.OrderBy(t => t.DialysisBedNo)*/.ToList();
                item.PrepareSumDetails = item.PrepareSumDetails.OrderBy(t => t.ItemClass).ToList();
            }
            dialysisPrepare.Summations = dialysisPrepare.Summations.OrderBy(t => t.ItemClass).ToList();
            return Content(await _patVisitApp.GetPrepareReport(dialysisPrepare));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Summery()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ObDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetReportImage([FromBody]BaseInput input)
        {
            var category = await CreateRecordCategories(input.KeyValue);
            return Content(await _patVisitApp.GetRecordReport(category));
        }

        [HttpPost]
        public async Task<IActionResult> GetReportImageBatch([FromBody]BaseInput input)
        {
            var arr = input.KeyValue.ToJArrayObject();
            //var list = await (arr.Select(item => item.Value<string>("F_Id")).Select(CreateRecordCategories));
            var list = new List<Category>();
            foreach (var item in arr.Select(item => item.Value<string>("F_Id")))
            {
                list.Add(await CreateRecordCategories(item));
            }
            return Content(await _patVisitApp.GetRecordReport(list));
        }

        /// <summary>
        /// 生成治疗单数据
        /// </summary>
        /// <param name="keyValue">治疗单ID号</param>
        /// <returns></returns>
        private async Task<Category> CreateRecordCategories(string keyValue)
        {
            var category = new Category
            {
                Orders = new List<Order>(),
                Observations = new List<Observation>(),
                PatVisitRecords = new List<PatVisitRecord>(),
                HospialName = await _organizeApp.GetHospitalName(),
                HospialLogo = await _organizeApp.GetHospitalLogo()
            };
            //OrganizeApp organizeApp = new OrganizeApp();
            var patvisit = await _patVisitApp.GetForm(keyValue);
            var patient = await _patientApp.GetForm(patvisit.F_Pid);
            category.Name = patient.F_Name;
            category.Gender = patient.F_Gender;
            category.AgeString = patient.F_BirthDay == null ? "   岁" : (DateTime.Now.Subtract((DateTime)patient.F_BirthDay).TotalDays.ToInt() / 365).ToString() + " 岁";
            category.IdealWeight = patient.F_IdealWeight == null ? "      " : patient.F_IdealWeight.ToString();
            category.VisitDate = patvisit.F_VisitDate.ToChineseDateString();
            category.DialysisNo = patient.F_DialysisNo.ToString();
            category.VisitNo = patvisit.F_VisitNo.ToInt().ToString();
            category.DialysisCount = _patVisitApp.GetList().Count(t=>t.F_Pid == patient.F_Id && t.F_VisitDate <= patvisit.F_VisitDate && t.F_EnabledMark == true).ToString();//"1"; //需统计透析次数patient.F_Id, patvisit.F_VisitDate.ToDate()
            category.BedNo = patvisit.F_GroupName + " " + patvisit.F_DialysisBedNo;
            PatVisitRecord record = new PatVisitRecord();
            category.PatVisitRecords.Add(record);

            string dialysisType = string.Empty;
            if (patvisit.F_DialysisType.Equals("HDF", StringComparison.InvariantCultureIgnoreCase))
            {
                dialysisType = patvisit.F_DialysisType + "    ( 稀释模式  " + patvisit.F_DilutionType
                               + "    置换液量  " + (patvisit.F_ExchangeAmount == null
                                   ? "     "
                                   : patvisit.F_ExchangeAmount.ToString())
                               + " L   " + (patvisit.F_ExchangeSpeed == null
                                   ? "       "
                                   : patvisit.F_ExchangeSpeed.ToFloat(2) + "") + " ml/min )";
            }
            record.DialysisType = string.IsNullOrEmpty(dialysisType) ? patvisit.F_DialysisType : dialysisType;
            record.DialyzerType1 = patvisit.F_DialyzerType1 == null
                ? ""
                : (await _materialApp.GetForm(patvisit.F_DialyzerType1))?.F_MaterialName ?? "";
            record.DialyzerType2 = patvisit.F_DialyzerType2 == null
                ? ""
                : (await _materialApp.GetForm(patvisit.F_DialyzerType2))?.F_MaterialName ?? "";
            record.EstimateHours = patvisit.F_EstimateHours == null ? "  " : patvisit.F_EstimateHours.ToString();
            record.BloodSpeed = patvisit.F_BloodSpeed == null ? "     " : patvisit.F_BloodSpeed.ToString();
            record.DialysateTemperature = patvisit.F_DialysateTemperature == null ? "     " : patvisit.F_DialysateTemperature.ToString();
            if (patvisit.F_HeparinType != null)
            {
                var drug = await _drugsApp.GetForm(patvisit.F_HeparinType);
                var heparinType = drug?.F_DrugName ?? "";
                if (patvisit.F_HeparinAmount != null)
                {
                    heparinType = heparinType + "    首剂   " + patvisit.F_HeparinAmount.ToString() + "  " + patvisit.F_HeparinUnit;
                    if (patvisit.F_HeparinAmount != null)
                    {
                        heparinType = heparinType + "  追加   " + patvisit.F_HeparinAddAmount.ToString() + "  " + patvisit.F_HeparinAddSpeedUnit;
                    }
                }
                record.HeparinType = heparinType;
            }
            record.VascularAccess = patvisit.F_VascularAccess + "    " + patvisit.F_AccessName;
            var vitalSigns = "血压 ";
            var systolicPressure = patvisit.F_SystolicPressure.ToInt();
            if (systolicPressure == 0)
            {
                vitalSigns = vitalSigns + "  /";
            }
            else
            {
                vitalSigns = vitalSigns + systolicPressure + "/ ";
            }
            var diastolicPressure = patvisit.F_DiastolicPressure.ToInt();
            if (diastolicPressure == 0)
            {
                vitalSigns = vitalSigns + "   mmHg";
            }
            else
            {
                vitalSigns = vitalSigns + diastolicPressure + "mmHg ";
            }
            vitalSigns = vitalSigns + " 脉搏 ";
            var pulse = patvisit.F_Pulse.ToInt();
            if (pulse == 0)
            {
                vitalSigns = vitalSigns + "     ";
            }
            else
            {
                vitalSigns = vitalSigns + " " + pulse + " ";
            }
            vitalSigns = vitalSigns + " 次/分";
            vitalSigns = vitalSigns + " 体温 ";
            var temperature = patvisit.F_Temperature.ToFloat(1);
            if (temperature <= 0)
            {
                vitalSigns = vitalSigns + "  ";
            }
            else
            {
                vitalSigns = vitalSigns + temperature + " ";
            }
            vitalSigns = vitalSigns + "℃";
            record.VitalSigns = vitalSigns;
            string pg = "外周水肿： ";
            pg = pg + (patvisit.F_PGwzsz ?? "       ")
                + "   心肺听诊：呼吸音   " + (patvisit.F_PGxftz1 ?? "       ")
                + "   HR： " + (string.IsNullOrEmpty(patvisit.F_PGxftz2) ? (pulse == 0 ? "       " : pulse.ToString()) : patvisit.F_PGxftz2)
                + " 次/分  " + (patvisit.F_PGxftz3 ?? "       ");
            record.PGwzsz = pg;
            string cx = patvisit.F_PGwzcx1 ?? "               ";
            if (cx.Equals("有"))
            {
                cx = cx + "   出血部位：" + (patvisit.F_PGwzcx2 ?? "           ");
            }
            cx = cx + " 其他：" + patvisit.F_PGother ?? "";
            record.PGwzcx = cx;
            record.WeightTQ = patvisit.F_WeightTQ == null ? "  " : patvisit.F_WeightTQ.ToString();
            record.WeightYT = patvisit.F_WeightYT == null ? "  " : patvisit.F_WeightYT.ToString();
            record.WeightTH = patvisit.F_WeightTH == null ? "  " : patvisit.F_WeightTH.ToString();

            record.Memo = patvisit.F_Memo;
            record.DJMH = patvisit.F_DJMH;
            record.DialyzerStatus = patvisit.F_DialyzerStatus;
            record.FistulaStatus = patvisit.F_FistulaStatus;
            record.DuctOpeningStatus = patvisit.F_DuctOpeningStatus;
            record.Reason = patvisit.F_Reason;
            if (patvisit.F_DialysisStartTime != null && patvisit.F_DialysisEndTime != null)
            {
                TimeSpan sp = patvisit.F_DialysisEndTime.ToDate() - patvisit.F_DialysisStartTime.ToDate();
                if (sp.TotalDays < 1 && sp.TotalHours > 0 && sp.TotalHours < 8)
                {
                    record.DialysisTime = sp.Hours + " h " + sp.Minutes + " min ";
                }
            }

            record.RealExchangeAmount = patvisit.F_RealExchangeAmount == null ? "    " : patvisit.F_RealExchangeAmount.ToString();
            record.MachineShowAmount = patvisit.F_MachineShowAmount == null ? "    " : patvisit.F_MachineShowAmount.ToString();
            //var user = new UserApp();
            record.PuncturePerson = patvisit.F_PuncturePerson == null
                ? ""
                : (await _usersService.FindUserAsync(patvisit.F_PuncturePerson))?.F_RealName ?? "";
            record.StartPerson = patvisit.F_StartPerson == null
                ? ""
                : (await _usersService.FindUserAsync(patvisit.F_StartPerson))?.F_RealName ?? "";
            record.CheckPerson = patvisit.F_CheckPerson == null
                ? ""
                : (await _usersService.FindUserAsync(patvisit.F_CheckPerson))?.F_RealName ?? "";
            record.EndPerson = patvisit.F_EndPerson == null
                ? ""
                : (await _usersService.FindUserAsync(patvisit.F_EndPerson))?.F_RealName ?? "";
            record.RecordDoctor = patvisit.F_RecordDoctor == null
                ? ""
                : (await _usersService.FindUserAsync(patvisit.F_RecordDoctor))?.F_RealName ?? "";


            var c = from r in await _ordersExecLogApp.GetList(patient.F_Id, patvisit.F_VisitDate.ToDate(), patvisit.F_VisitDate.ToDate().AddDays(1))
                    select new
                    {
                        OrderType = r.F_OrderType,
                        OrderText = r.F_OrderText,
                        OrderSpec = r.F_OrderSpec,
                        OrderUnitAmount = r.F_OrderType.Equals("r.F_OrderType") ? r.F_OrderUnitAmount : "",
                        OrderUnitSpec = r.F_OrderUnitSpec,
                        OrderAmount = r.F_OrderAmount,
                        OrderFrequency = r.F_OrderFrequency,
                        OrderAdministration = r.F_OrderAdministration,
                        IsTemporary = r.F_IsTemporary,
                        Doctor = r.F_Doctor,
                        //DoctorAuditTime = r.F_DoctorAuditTime.ToChineseDateString(),
                        DoctorAuditTime = r.F_NurseOperatorTime.ToChineseDateString(),
                        Nurse = r.F_Nurse,
                        NurseOperatorTime = r.F_NurseOperatorTime.ToTimeString(true),
                        Memo = r.F_Memo
                    };
            int count = 0;
            foreach (var item in c)
            {
                count++;
                if (count > 5) break;
                Order order = new Order
                {
                    OrderType = item.OrderType,
                    OrderText = item.OrderText,
                    OrderSpec = item.OrderSpec,
                    OrderUnitAmount = item.OrderUnitAmount,
                    OrderUnitSpec = item.OrderUnitSpec,
                    OrderAmount = item.OrderAmount.ToString(),
                    OrderFrequency = item.OrderFrequency,
                    OrderAdministration = item.OrderAdministration,
                    IsTemporary = item.IsTemporary,
                    Doctor = item.Doctor,
                    DoctorAuditTime = item.DoctorAuditTime,
                    Nurse = item.Nurse,
                    NurseOperatorTime = item.NurseOperatorTime,
                    Memo = item.Memo
                };
                category.Orders.Add(order);
            }
            for (int i = category.Orders.Count; i < 5; i++)
            {
                Order order = new Order
                {
                    OrderType = null,
                    OrderText = null,
                    OrderSpec = null,
                    OrderUnitAmount = null,
                    OrderUnitSpec = null,
                    OrderAmount = null,
                    OrderFrequency = null,
                    OrderAdministration = null,
                    IsTemporary = null,
                    Doctor = null,
                    DoctorAuditTime = null,
                    Nurse = null,
                    NurseOperatorTime = null,
                    Memo = null
                };
                category.Orders.Add(order);
            }
            //DialysisObservationApp obapp = new DialysisObservationApp();
            //var oblist = obapp.GetObservationsByDate(patvisit.F_VisitDate.ToDate(), patvisit.F_VisitDate.ToDate().AddDays(1), patient.F_DialysisNo);patvisit.F_VisitDate.ToDate(), patvisit.F_VisitDate.ToDate().AddDays(1)
            var obDatas = from r in _dialysisObservationApp.GetList(patient.F_Id)
                          where r.F_NurseOperatorTime >= patvisit.F_VisitDate.ToDate()
                          where r.F_NurseOperatorTime <= patvisit.F_VisitDate.ToDate().AddDays(1)
                          select new
                          {
                              SSY = r.F_SSY.ToFloat(2),
                              SZY = r.F_SZY.ToFloat(2),
                              HR = r.F_HR.ToFloat(2),
                              A = r.F_A.ToFloat(2),
                              BF = r.F_BF.ToFloat(2),
                              UFR = r.F_UFR.ToFloat(2),
                              V = r.F_V.ToFloat(2),
                              C = r.F_C.ToFloat(2),
                              T = r.F_T.ToFloat(2),
                              UFV = r.F_UFV.ToFloat(2),
                              TMP = r.F_TMP.ToFloat(2),
                              GSL = r.F_GSL.ToFloat(2),
                              MEMO = r.F_MEMO,
                              Nurse = r.F_Nurse,
                              NurseOperatorTime = r.F_NurseOperatorTime.ToDate().ToTimeString(true)
                          };
            count = 0;
            foreach (var item in obDatas)
            {
                count++;
                if (count > 8) break;
                Observation ob = new Observation
                {
                    SSY = item.SSY.ToString() == "0" ? "" : item.SSY.ToString(),
                    SZY = item.SZY.ToString() == "0" ? "" : item.SZY.ToString(),
                    HR = item.HR.ToString() == "0" ? "" : item.HR.ToString(),
                    A = item.A.ToString() == "0" ? "" : item.A.ToString(),
                    BF = item.BF.ToString() == "0" ? "" : item.BF.ToString(),
                    UFR = item.UFR.ToString() == "0" ? "" : item.UFR.ToString(),
                    V = item.V.ToString() == "0" ? "" : item.V.ToString(),
                    C = item.C.ToString() == "0" ? "" : item.C.ToString(),
                    T = item.T.ToString() == "0" ? "" : item.T.ToString(),
                    UFV = item.UFV.ToString() == "0" ? "" : item.UFV.ToString(),
                    TMP = item.TMP.ToString() == "0" ? "" : item.TMP.ToString(),
                    GSL = item.GSL.ToString() == "0" ? "" : item.GSL.ToString(),
                    MEMO = item.MEMO ?? "",
                    Nurse = item.Nurse ?? "",
                    NurseOperatorTime = item.NurseOperatorTime.ToString()
                };
                category.Observations.Add(ob);
            }

            for (int i = category.Observations.Count; i < 8; i++)
            {
                category.Observations.Add(new Observation());
            }

            return category;
        }

        [HttpPost]
        public async Task<IActionResult> GetReportImageBatchTQ([FromBody]BaseInput input)
        {
            var arr = input.KeyValue.ToJArrayObject();
            //var list = arr.Select(item => item.Value<string>("F_Id")).Select(CreateRecordCategoriesTQ).ToList();
            var list = new List<Category>();
            foreach (var item in arr.Select(item => item.Value<string>("F_Id")))
            {
                list.Add(await CreateRecordCategoriesTQ(item));
            }
            return Content(await _patVisitApp.GetRecordReportTQ(list));
        }

        /// <summary>
        /// 生成透前治疗单数据
        /// </summary>
        /// <param name="keyValue">治疗单ID号</param>
        /// <returns></returns>
        private async Task<Category> CreateRecordCategoriesTQ(string keyValue)
        {
            var category = new Category
            {
                Orders = new List<Order>(),
                Observations = new List<Observation>(),
                PatVisitRecords = new List<PatVisitRecord>(),
                HospialName = await _organizeApp.GetHospitalName(),
                HospialLogo = await _organizeApp.GetHospitalLogo()
            };
            //OrganizeApp organizeApp = new OrganizeApp();
            var patvisit = await _patVisitApp.GetForm(keyValue);
            var patient = await _patientApp.GetForm(patvisit.F_Pid);
            category.Name = patient.F_Name;
            category.Gender = patient.F_Gender;
            category.AgeString = patient.F_BirthDay == null ? "   岁" : (DateTime.Now.Subtract((DateTime)patient.F_BirthDay).TotalDays.ToInt() / 365).ToString() + " 岁";
            category.IdealWeight = patient.F_IdealWeight == null ? "  " : patient.F_IdealWeight.ToString();
            category.VisitDate = patvisit.F_VisitDate.ToChineseDateString();
            category.DialysisNo = patient.F_DialysisNo.ToString();
            category.VisitNo = patvisit.F_VisitNo.ToInt().ToString();
            category.DialysisCount = _patVisitApp.GetList().Count(t => t.F_Pid == patient.F_Id && t.F_VisitDate <= patvisit.F_VisitDate && t.F_EnabledMark == true).ToString();//"1"; //需统计透析次数
            category.BedNo = patvisit.F_GroupName + " " + patvisit.F_DialysisBedNo;
            PatVisitRecord record = new PatVisitRecord();
            category.PatVisitRecords.Add(record);

            string dialysisType = string.Empty;
            if (patvisit.F_DialysisType.Equals("HDF", StringComparison.InvariantCultureIgnoreCase))
            {
                dialysisType = patvisit.F_DialysisType + "    ( 稀释模式  " + patvisit.F_DilutionType
                               + "    置换液量  " + (patvisit.F_ExchangeAmount == null
                                   ? "     "
                                   : patvisit.F_ExchangeAmount.ToString())
                               + " L   " + (patvisit.F_ExchangeSpeed == null
                                   ? "       "
                                   : patvisit.F_ExchangeSpeed.ToFloat(2) + "") + " ml/min )";
            }
            record.DialysisType = string.IsNullOrEmpty(dialysisType) ? patvisit.F_DialysisType : dialysisType;
            record.DialyzerType1 = patvisit.F_DialyzerType1 == null ? "" : (await _materialApp.GetForm(patvisit.F_DialyzerType1))?.F_MaterialName;
            record.DialyzerType2 = patvisit.F_DialyzerType2 == null ? "" : (await _materialApp.GetForm(patvisit.F_DialyzerType2))?.F_MaterialName;
            record.EstimateHours = patvisit.F_EstimateHours == null ? "  " : patvisit.F_EstimateHours.ToString();
            record.BloodSpeed = patvisit.F_BloodSpeed == null ? "     " : patvisit.F_BloodSpeed.ToString();
            record.DialysateTemperature = patvisit.F_DialysateTemperature == null ? "     " : patvisit.F_DialysateTemperature.ToString();
            if (patvisit.F_HeparinType != null)
            {
                var drug = await _drugsApp.GetForm(patvisit.F_HeparinType);
                string heparinType = drug.F_DrugName;
                if (patvisit.F_HeparinAmount != null)
                {
                    heparinType = heparinType + "    首剂   " + patvisit.F_HeparinAmount.ToString() + "  " + patvisit.F_HeparinUnit;
                    if (patvisit.F_HeparinAmount != null)
                    {
                        heparinType = heparinType + "  追加   " + patvisit.F_HeparinAddAmount.ToString() + "  " + patvisit.F_HeparinAddSpeedUnit;
                    }
                }
                record.HeparinType = heparinType;
            }
            record.VascularAccess = patvisit.F_VascularAccess + "    " + patvisit.F_AccessName;
            string vitalSigns = "血压 ";
            int systolicPressure = patvisit.F_SystolicPressure.ToInt();
            if (systolicPressure == 0)
            {
                vitalSigns = vitalSigns + "  /";
            }
            else
            {
                vitalSigns = vitalSigns + systolicPressure + "/ ";
            }
            int diastolicPressure = patvisit.F_DiastolicPressure.ToInt();
            if (diastolicPressure == 0)
            {
                vitalSigns = vitalSigns + "   mmHg";
            }
            else
            {
                vitalSigns = vitalSigns + diastolicPressure + "mmHg ";
            }
            vitalSigns = vitalSigns + " 脉搏 ";
            int pulse = patvisit.F_Pulse.ToInt();
            if (pulse == 0)
            {
                vitalSigns = vitalSigns + "     ";
            }
            else
            {
                vitalSigns = vitalSigns + " " + pulse + " ";
            }
            vitalSigns = vitalSigns + " 次/分";
            vitalSigns = vitalSigns + " 体温 ";
            float temperature = patvisit.F_Temperature.ToFloat(1);
            if (temperature <= 0)
            {
                vitalSigns = vitalSigns + "  ";
            }
            else
            {
                vitalSigns = vitalSigns + temperature + " ";
            }
            vitalSigns = vitalSigns + "℃";
            record.VitalSigns = vitalSigns;
            string pg = "外周水肿： ";
            pg = pg + (patvisit.F_PGwzsz ?? "       ")
                + "   心肺听诊：呼吸音   " + (patvisit.F_PGxftz1 ?? "       ")
                + "   HR： " + (string.IsNullOrEmpty(patvisit.F_PGxftz2) ? (pulse == 0 ? "       " : pulse.ToString()) : patvisit.F_PGxftz2)
                + " 次/分  " + (patvisit.F_PGxftz3 ?? "       ");
            record.PGwzsz = pg;
            string cx = patvisit.F_PGwzcx1 ?? "               ";
            if (cx.Equals("有"))
            {
                cx = cx + "   出血部位：" + (patvisit.F_PGwzcx2 ?? "           ");
            }
            cx = cx + " 其他：" + patvisit.F_PGother ?? "";
            record.PGwzcx = cx;
            record.WeightTQ = patvisit.F_WeightTQ == null ? "  " : patvisit.F_WeightTQ.ToString();
            record.WeightYT = patvisit.F_WeightYT == null ? "  " : patvisit.F_WeightYT.ToString();
            record.WeightTH = patvisit.F_WeightTH == null ? "  " : patvisit.F_WeightTH.ToString();

            record.Memo = patvisit.F_Memo;
            record.DJMH = patvisit.F_DJMH;
            record.DialyzerStatus = patvisit.F_DialyzerStatus;
            record.FistulaStatus = patvisit.F_FistulaStatus;
            record.DuctOpeningStatus = patvisit.F_DuctOpeningStatus;
            record.Reason = patvisit.F_Reason;
            if (patvisit.F_DialysisStartTime != null && patvisit.F_DialysisEndTime != null)
            {
                TimeSpan sp = patvisit.F_DialysisEndTime.ToDate() - patvisit.F_DialysisStartTime.ToDate();
                if (sp.TotalDays < 1 && sp.TotalHours > 0 && sp.TotalHours < 8)
                {
                    record.DialysisTime = sp.Hours + " h " + sp.Minutes + " min ";
                }
            }

            record.RealExchangeAmount = patvisit.F_RealExchangeAmount == null ? "    " : patvisit.F_RealExchangeAmount.ToString();
            record.MachineShowAmount = patvisit.F_MachineShowAmount == null ? "    " : patvisit.F_MachineShowAmount.ToString();
            //var user = new UserApp();
            record.PuncturePerson = patvisit.F_PuncturePerson == null ? "" : (await _usersService.FindUserAsync(patvisit.F_PuncturePerson)).F_RealName;
            record.StartPerson = patvisit.F_StartPerson == null ? "" : (await _usersService.FindUserAsync(patvisit.F_StartPerson)).F_RealName;
            record.CheckPerson = patvisit.F_CheckPerson == null ? "" : (await _usersService.FindUserAsync(patvisit.F_CheckPerson)).F_RealName;
            record.EndPerson = patvisit.F_EndPerson == null ? "" : (await _usersService.FindUserAsync(patvisit.F_EndPerson)).F_RealName;
            record.RecordDoctor = patvisit.F_RecordDoctor == null ? "" : (await _usersService.FindUserAsync(patvisit.F_RecordDoctor)).F_RealName;


            var c = from r in await _ordersApp.GetList(patient.F_Id, patvisit.F_VisitDate.ToDate(), patvisit.F_VisitDate.ToDate().AddDays(1))
                    select new
                    {
                        OrderType = r.F_OrderType,
                        OrderText = r.F_OrderText,
                        OrderSpec = r.F_OrderSpec,
                        OrderUnitAmount = r.F_OrderType.Equals("r.F_OrderType") ? r.F_OrderUnitAmount : "",
                        OrderUnitSpec = r.F_OrderUnitSpec,
                        OrderAmount = r.F_OrderAmount,
                        OrderFrequency = r.F_OrderFrequency,
                        OrderAdministration = r.F_OrderAdministration,
                        IsTemporary = r.F_IsTemporary,
                        Doctor = r.F_Doctor,
                        //DoctorAuditTime = r.F_DoctorAuditTime.ToChineseDateString(),
                        DoctorAuditTime = "",//r.F_NurseOperatorTime.ToChineseDateString(),
                        Nurse = "",//r.F_Nurse,
                        NurseOperatorTime = "",// r.F_NurseOperatorTime.ToTimeString(true),
                        Memo = r.F_Memo,
                        r.F_DoctorOrderTime,
                        r.F_SubIndex
                    };
            int count = 0;
            foreach (var item in c.OrderBy(t => t.F_DoctorOrderTime).ThenBy(t => t.F_SubIndex))
            {
                count++;
                if (count > 5) break;
                Order order = new Order
                {
                    OrderType = item.OrderType,
                    OrderText = item.OrderText,
                    OrderSpec = item.OrderSpec,
                    OrderUnitAmount = item.OrderUnitAmount,
                    OrderUnitSpec = item.OrderUnitSpec,
                    OrderAmount = item.OrderAmount.ToString(),
                    OrderFrequency = item.OrderFrequency,
                    OrderAdministration = item.OrderAdministration,
                    IsTemporary = item.IsTemporary,
                    Doctor = item.Doctor,
                    DoctorAuditTime = item.DoctorAuditTime,
                    Nurse = item.Nurse,
                    NurseOperatorTime = item.NurseOperatorTime,
                    Memo = item.Memo
                };
                category.Orders.Add(order);
            }
            for (int i = category.Orders.Count; i < 5; i++)
            {
                Order order = new Order
                {
                    OrderType = null,
                    OrderText = null,
                    OrderSpec = null,
                    OrderUnitAmount = null,
                    OrderUnitSpec = null,
                    OrderAmount = null,
                    OrderFrequency = null,
                    OrderAdministration = null,
                    IsTemporary = null,
                    Doctor = null,
                    DoctorAuditTime = null,
                    Nurse = null,
                    NurseOperatorTime = null,
                    Memo = null
                };
                category.Orders.Add(order);
            }
            //var oblist = obapp.GetObservationsByDate(patvisit.F_VisitDate.ToDate(), patvisit.F_VisitDate.ToDate().AddDays(1), patient.F_DialysisNo);
            var obDatas =
                from r in
                    _dialysisObservationApp
                        .GetList(patient.F_Id)
                where r.F_NurseOperatorTime >= patvisit.F_VisitDate.ToDate()
                where r.F_NurseOperatorTime <= patvisit.F_VisitDate.ToDate().AddDays(1)
                select new
                {
                    SSY = r.F_SSY.ToFloat(2),
                    SZY = r.F_SZY.ToFloat(2),
                    HR = r.F_HR.ToFloat(2),
                    A = r.F_A.ToFloat(2),
                    BF = r.F_BF.ToFloat(2),
                    UFR = r.F_UFR.ToFloat(2),
                    V = r.F_V.ToFloat(2),
                    C = r.F_C.ToFloat(2),
                    T = r.F_T.ToFloat(2),
                    UFV = r.F_UFV.ToFloat(2),
                    TMP = r.F_TMP.ToFloat(2),
                    GSL = r.F_GSL.ToFloat(2),
                    MEMO = r.F_MEMO,
                    Nurse = r.F_Nurse,
                    NurseOperatorTime = r.F_NurseOperatorTime.ToDate().ToTimeString(true)
                };
            count = 0;
            foreach (var item in obDatas)
            {
                count++;
                if (count > 8) break;
                Observation ob = new Observation
                {
                    SSY = item.SSY.ToString() == "0" ? "" : item.SSY.ToString(),
                    SZY = item.SZY.ToString() == "0" ? "" : item.SZY.ToString(),
                    HR = item.HR.ToString() == "0" ? "" : item.HR.ToString(),
                    A = item.A.ToString() == "0" ? "" : item.A.ToString(),
                    BF = item.BF.ToString() == "0" ? "" : item.BF.ToString(),
                    UFR = item.UFR.ToString() == "0" ? "" : item.UFR.ToString(),
                    V = item.V.ToString() == "0" ? "" : item.V.ToString(),
                    C = item.C.ToString() == "0" ? "" : item.C.ToString(),
                    T = item.T.ToString() == "0" ? "" : item.T.ToString(),
                    UFV = item.UFV.ToString() == "0" ? "" : item.UFV.ToString(),
                    TMP = item.TMP.ToString() == "0" ? "" : item.TMP.ToString(),
                    GSL = item.GSL.ToString() == "0" ? "" : item.GSL.ToString(),
                    MEMO = item.MEMO ?? "",
                    Nurse = item.Nurse ?? "",
                    NurseOperatorTime = item.NurseOperatorTime.ToString()
                };
                category.Observations.Add(ob);
            }

            for (int i = category.Observations.Count; i < 8; i++)
            {
                category.Observations.Add(new Observation());
            }

            return category;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCreatePrepare([FromBody]BaseInput input)
        {
            var dialysisPrepare = new DialysisPrepare
            {
                PrepareRecords = new List<PrepareRecord>(),
                Summations = new List<Summation>(),
                HospialName = await _organizeApp.GetHospitalName(),
                HospialLogo = await _organizeApp.GetHospitalLogo(),
            };
         
            var drugDict = new Dictionary<string, string>();
            var materialDict = new Dictionary<string, string>();

            var jArr = input.KeyValue.ToJArrayObject();

            var list = from r in _patVisitApp.GetList()
                       join f in jArr on r.F_Id equals f.Value<string>("F_Id")
                       select new
                       {
                           VisitDate = r.F_VisitDate.ToChineseDateString(),
                           VisitNo = r.F_VisitNo.ToString(), //班次
                           Name = r.F_Name,
                           DialysisNo = r.F_DialysisNo.ToString(),
                           GroupName = r.F_GroupName,
                           DialysisBedNo = r.F_DialysisBedNo,
                           DialysisType = r.F_DialysisType,
                           DialyzerType1 = r.F_DialyzerType1,
                           DialyzerType2 = r.F_DialyzerType2,
                           VascularAccess = r.F_VascularAccess,
                           AccessName = r.F_AccessName,
                           HeparinType = r.F_HeparinType,
                           HeparinAmount = r.F_HeparinAmount,
                           HeparinUnit = r.F_HeparinUnit,
                           HeparinAddAmount = r.F_HeparinAddAmount,
                           HeparinAddSpeedUnit = r.F_HeparinAddSpeedUnit
                       };
            foreach (var item in list)
            {
                var prepareRecord = dialysisPrepare.PrepareRecords.Find(t => t.VisitNo.Equals(item.VisitNo));
                if (prepareRecord == null)
                {
                    prepareRecord = new PrepareRecord
                    {
                        VisitDate = item.VisitDate,
                        VisitNo = item.VisitNo,
                        PrepareRecordDetails = new List<PrepareRecordDetail>(),
                        PrepareSumDetails = new List<PrepareSumDetail>()
                    };
                    dialysisPrepare.PrepareRecords.Add(prepareRecord);
                }
                var key = string.Empty;
                if (item.HeparinType != null)
                {
                    key = item.HeparinType;
                    if (!drugDict.ContainsKey(key))
                    {
                        drugDict[key] = (await _drugsApp.GetForm(key))?.F_DrugName ?? "";
                    }
                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName.Equals(drugDict[key]));
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "抗凝剂",
                            ItemName = drugDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName.Equals(drugDict[key]));
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "抗凝剂",
                            ItemName = drugDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }
                if (item.DialyzerType1 != null)
                {
                    key = item.DialyzerType1;
                    if (!materialDict.ContainsKey(key))
                    {
                        materialDict[key] = (await _materialApp.GetForm(key))?.F_MaterialName ?? "";
                    }
                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName.Equals(materialDict[key]));
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName.Equals(materialDict[key]));
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }
                if (item.DialyzerType2 != null)
                {
                    key = item.DialyzerType2;
                    if (!materialDict.ContainsKey(key))
                    {
                        materialDict[key] = (await _materialApp.GetForm(key))?.F_MaterialName ?? "";
                    }
                    var prepareSumDetail = prepareRecord.PrepareSumDetails.Find(t => t.ItemName.Equals(materialDict[key]));
                    if (prepareSumDetail == null)
                    {
                        prepareSumDetail = new PrepareSumDetail
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        prepareRecord.PrepareSumDetails.Add(prepareSumDetail);
                    }
                    prepareSumDetail.Amount++;
                    var summation = dialysisPrepare.Summations.Find(t => t.ItemName.Equals(materialDict[key]));
                    if (summation == null)
                    {
                        summation = new Summation
                        {
                            ItemClass = "耗材",
                            ItemName = materialDict[key],
                            Amount = 0
                        };
                        dialysisPrepare.Summations.Add(summation);
                    }
                    summation.Amount++;
                }

                prepareRecord.PrepareRecordDetails.Add(new PrepareRecordDetail
                {
                    Name = item.Name,
                    DialysisNo = item.DialysisNo,
                    GroupName = item.GroupName,
                    DialysisBedNo = item.DialysisBedNo,
                    DialysisType = item.DialysisType,
                    DialyzerType1 = item.DialyzerType1 == null ? "" : materialDict[item.DialyzerType1],
                    DialyzerType2 = item.DialyzerType2 == null ? "" : materialDict[item.DialyzerType2],
                    VascularAccess = item.VascularAccess,
                    AccessName = item.AccessName,
                    HeparinType = item.HeparinType == null ? "" : drugDict[item.HeparinType],
                    HeparinAmount = item.HeparinAmount.ToString(),
                    HeparinUnit = item.HeparinUnit,
                    HeparinAddAmount = item.HeparinAddAmount.ToString(),
                    HeparinAddSpeedUnit = item.HeparinAddSpeedUnit
                });
            }
            //排序
            foreach (var item in dialysisPrepare.PrepareRecords)
            {
                item.PrepareRecordDetails = item.PrepareRecordDetails.OrderBy(t => t.DialysisBedNo).ToList();
                item.PrepareSumDetails = item.PrepareSumDetails.OrderBy(t => t.ItemClass).ToList();
            }
            dialysisPrepare.Summations = dialysisPrepare.Summations.OrderBy(t => t.ItemClass).ToList();

            return Content(await _patVisitApp.GetPrepareReport(dialysisPrepare));
            //return Content(app.GetRecordReport(category));
        }

        public async Task<IActionResult> GetSubmitOrderJson(string keyValue, DateTime startDate, DateTime endDate)
        {
            var c = from r in (await _ordersApp.GetList(keyValue, startDate, endDate)).OrderBy(t => t.F_OrderType)
                    select new
                    {
                        F_IsTemporary = r.F_IsTemporary == null ? "临时" : ((bool)r.F_IsTemporary) ? "长期" : "临时",
                        F_Nurse = r.F_Nurse ?? "",
                        F_NurseOperatorTime = r.F_NurseOperatorTime == null ? "" : ((DateTime)r.F_NurseOperatorTime).ToString("MM-dd HH:mm"),
                        r.F_OrderCode,
                        r.F_OrderSpec,
                        r.F_OrderStatus,
                        r.F_OrderType,
                        F_OrderText = r.F_OrderText + (r.F_OrderAdministration == null ? "" : "  [" + r.F_OrderAdministration + "]"),
                        F_OrderAmount =  r.F_OrderAmount + (r.F_OrderUnitSpec == null ? "" : "(" + r.F_OrderUnitSpec + ")"),
                        F_OrderFrequency = "" + r.F_OrderFrequency + "  " + r.F_Memo,
                        r.F_DoctorAuditTime,
                        r.F_Id
                    };
            return Content(c.ToJson());
        }

        public IActionResult GetWeightCharts(string keyValue)
        {
            //取最近的8次治疗记录
            var data = (from r in _patVisitApp.GetList()
                where r.F_Pid == keyValue
                orderby r.F_VisitDate
                select new
                {
                    date = r.F_VisitDate.ToDateString(),
                    tq = r.F_WeightTQ.ToFloat(2),
                    th = r.F_WeightTH.ToFloat(2),
                    st = r.F_WeightST.ToFloat(2),
                    yt = r.F_WeightYT.ToFloat(2)
                }).Take(8).ToList();
            return Content(data.ToJson());
        }
        /// <summary>
        /// 根据治疗单主键查询称重记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetWeightLogs(string keyValue)
        {
            var data = _patVisitApp.GetWeightLogList(keyValue).Select(t => new
            {
                t.F_CreatorTime,
                t.F_Value
            })
            .OrderByDescending(t => t.F_CreatorTime)
            .ToList()
            .Select(t => new
            {
                time = t.F_CreatorTime.ToDateTimeString(true).Split(' ')[1],
                value = t.F_Value.ToFloat(2)
            });
            return Content(data.ToJson());
        }

        public IActionResult GetWeightJson(string keyValue)
        {
            var json = keyValue.ToJObject();
            var startDate = json.Value<DateTime>("startDate");
            var endDate = json.Value<DateTime>("endDate");
            var pid = json.Value<string>("pid");

            //查询治疗记录
            var data = (from r in _patVisitApp.GetList()
                        where r.F_Pid == pid && r.F_VisitDate >= startDate && r.F_VisitDate <= endDate
                        orderby r.F_VisitDate
                        select new
                        {
                            date = r.F_VisitDate.ToDateString().Substring(5),
                            tq = r.F_WeightTQ.ToFloat(2),
                            th = r.F_WeightTH.ToFloat(2),
                            st = r.F_WeightST.ToFloat(2),
                            yt = r.F_WeightYT.ToFloat(2)
                        }).ToList();
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetPatVisitListJson(DateTime visitDate)
        {
            var list = _patVisitApp.GetList()
                .Where(t => t.F_VisitDate == visitDate)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_Pid,
                    t.F_GroupName,
                    t.F_DialysisBedNo,
                    t.F_HeparinType,
                    t.F_VisitDate,
                    t.F_VisitNo,
                    t.F_DialysisType,
                    t.F_DialyzerType1,
                    t.F_DialyzerType2,
                    t.F_VascularAccess,
                    t.F_DialysisStartTime,
                    t.F_DialysisEndTime,
                    t.F_StartPerson,
                    t.F_EndPerson,
                    t.F_CheckPerson,
                    t.F_PuncturePerson,
                    t.F_IsAcct,
                    t.F_IsArchive
                }).ToList();
            //患者
            var p = (from c in _patientApp.GetQueryable()
                where list.Any(t => t.F_Pid == c.F_Id)
                select new {c.F_Id, c.F_Name, c.F_Gender, c.F_BirthDay, c.F_DialysisNo, c.F_PY}).ToList();
            //耗材
            var m = (from c in await _materialApp.GetList()
                where list.Any(t => c.F_Id == t.F_DialyzerType1 || c.F_Id == t.F_DialyzerType2)
                select new
                {
                    c.F_Id,
                    c.F_MaterialName
                }).ToList();
            //药品
            var d = (from c in await _drugsApp.GetList()
                where list.Any(t => c.F_Id == t.F_HeparinType)
                select new
                {
                    c.F_Id,
                    //c.F_DrugCode,
                    c.F_DrugName
                }).ToList();


            //查询治疗记录
            var data = new List<PatVisitListResult>();
            foreach (var item in list)
            {
                var temp = new PatVisitListResult
                {
                    Id = item.F_Id,
                    Pid = item.F_Pid,
                    GroupName = item.F_GroupName,
                    DialysisBedNo = item.F_DialysisBedNo
                };
                var patient = p.FirstOrDefault(t => t.F_Id == item.F_Pid);
                if (patient == null) continue;
                temp.Name = patient.F_Name ?? "";
                temp.Gender = patient.F_Gender ?? "";
                temp.VisitDate = item.F_VisitDate.ToDateString();
                temp.VisitNo = item.F_VisitNo.ToInt();
                temp.DialysisNo = patient.F_DialysisNo.ToInt();
                temp.DialysisType = item.F_DialysisType ?? "";
                temp.DialyzerType1 = item.F_DialyzerType1 == null ? "" : m.First(t => t.F_Id.Equals(item.F_DialyzerType1)).F_MaterialName;
                temp.DialyzerType2 = item.F_DialyzerType2 == null ? "" : m.First(t => t.F_Id.Equals(item.F_DialyzerType2)).F_MaterialName;
                temp.VascularAccess = item.F_VascularAccess ?? "";
                temp.HeparinType = item.F_HeparinType == null ? "" : d.First(t => t.F_Id.Equals(item.F_HeparinType)).F_DrugName;
                temp.BirthDay = patient.F_BirthDay.ToDateString();
                temp.AgeDesc = patient.F_BirthDay == null ? "" : ((int)((DateTime.Now - patient.F_BirthDay.ToDate()).TotalDays / 365)).ToString() + "岁";
                temp.StartTime = item.F_DialysisStartTime == null ? "" : item.F_DialysisStartTime.ToDateTimeString();
                temp.EndTime = item.F_DialysisEndTime == null ? "" : item.F_DialysisEndTime.ToDateTimeString();
                temp.Py = (patient.F_PY ?? "").ToLower();
                temp.StartPerson = item.F_StartPerson;
                temp.EndPerson = item.F_EndPerson;
                temp.CheckPerson = item.F_CheckPerson;
                temp.PuncturePerson = item.F_PuncturePerson;
                temp.IsArchive = item.F_IsArchive == true;
                temp.IsAcct = item.F_IsAcct == true;
                data.Add(temp);
            }
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SaveOperateMachineTime([FromBody]BaseInput input)
        {
            var json = input.KeyValue.ToJObject();
            var id = json.Value<string>("id");
            if (!DateTime.TryParse(json.Value<string>("operateTime"), out var operateTime)) operateTime = DateTime.Now;
            var operateType = json.Value<string>("operateType");

            await _patVisitApp.SetOperateMachineTime(id, operateTime, operateType);
            return Success("操作成功");
        }
        /// <summary>
        /// 医生填写透前评估信息 签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DoctorSign([FromBody]BaseInput input)
        {
            await _patVisitApp.DoctorSign(input.KeyValue);
            return Success("操作成功");
        }

        [HttpPost]
        public async Task<IActionResult> ModifyYTWeight([FromBody]ModifyYTWeightInput input)
        {
            //var entity = await _patVisitApp.GetForm(keyValue);
            //if (entity == null) return Error("记录单主键有误！");
            if (input.Value < 0 || input.Value > 5) return Error("预脱量设置有误！");
            if (_patVisitApp.GetArchiveStatus(input.Keyvalue))
            {
                return Error("已归档。");
            }
            //entity.F_WeightYT = value;
            await _patVisitApp.UpdateYTWeight(input.Keyvalue, input.Value);
            return Success("操作成功");
        }

        /// <summary>
        /// 护士核对透析治疗单 ，打印卡片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetNurseCheckReport([FromBody]BaseInput input)
        {
            var json = input.KeyValue.ToJObject();
            var visitDateStr = json.Value<string>("visitDate");
            if (string.IsNullOrEmpty(visitDateStr)) return Error("透析日期未传值！");
            if (!DateTime.TryParse(visitDateStr, out DateTime visitDate))
            {
                visitDate = DateTime.Now.Date;
            }

            var groupName = json.Value<string>("groupName") ?? "";
            var visitNo = json.Value<int>("visitNo");
            var list = _patVisitApp.GetList(visitDate,visitNo)
                .Where(t => groupName == "" || t.F_GroupName == groupName)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_VisitNo,
                    t.F_Name,
                    t.F_GroupName,
                    t.F_DialysisBedNo,
                    t.F_DialysisType,
                    t.F_HeparinAddAmount,
                    t.F_HeparinAddSpeedUnit,
                    t.F_HeparinAmount,
                    t.F_HeparinType,
                    t.F_HeparinUnit,
                    t.F_DialyzerType1,
                    t.F_DialyzerType2,
                    t.F_CheckPerson
                }).ToList();
            if (list.Count == 0) return Error("无透析记录！");
            var heparinList = await _drugsApp.GetList("");
            var dialyzerList = await _materialApp.GetList("");
            var records = new List<DialysisCheckRecord>();
            foreach (var item in list.Select(t => new {
                t.F_VisitDate,
                t.F_VisitNo,
                t.F_Name,
                t.F_GroupName,
                t.F_DialysisBedNo,
                t.F_DialysisType,
                t.F_HeparinAddAmount,
                t.F_HeparinAddSpeedUnit,
                t.F_HeparinAmount,
                t.F_HeparinType,
                t.F_HeparinUnit,
                t.F_DialyzerType1,
                t.F_DialyzerType2,
                t.F_CheckPerson,
                orderNo = int.TryParse(t.F_DialysisBedNo, out int order) ? order : 999
            }).OrderBy(t => t.F_GroupName).ThenBy(t => t.F_VisitNo).ThenBy(t => t.orderNo))
            {
                var find = records.Find(t => t.VisitDate == item.F_VisitDate && t.VisitNo == item.F_VisitNo && t.GroupName.Equals(item.F_GroupName));
                if (find == null)
                {
                    find = new DialysisCheckRecord
                    {
                        VisitDate = item.F_VisitDate.ToDate(),
                        VisitNo = item.F_VisitNo.ToInt(),
                        GroupName = item.F_GroupName
                    };
                    records.Add(find);
                }

                var heparin = heparinList.FirstOrDefault(t => t.F_Id == item.F_HeparinType);
                var dialyzer1 = dialyzerList.FirstOrDefault(t => t.F_Id == item.F_DialyzerType1);
                var dialyzer2 = dialyzerList.FirstOrDefault(t => t.F_Id == item.F_DialyzerType2);
                if (find.Items == null) find.Items = new List<CheckRecordItem>();
                find.Items.Add(new CheckRecordItem
                {
                    Name = item.F_Name,
                    BedNo = item.F_DialysisBedNo,
                    DialysisType = item.F_DialysisType,
                    HeparinType = heparin == null ? "" : heparin.F_DrugName,
                    HeparinAmount = item.F_HeparinAmount == null ? "" : item.F_HeparinAmount.ToString(),
                    HeparinUnit = item.F_HeparinUnit,
                    HeparinAddAmount = item.F_HeparinAddAmount == null ? "" : item.F_HeparinAddAmount.ToString(),
                    DialyzerType1 = dialyzer1 == null ? "" : dialyzer1.F_MaterialName,
                    DialyzerType2 = dialyzer2 == null ? "" : dialyzer2.F_MaterialName
                });
            }
            return Content(await _patVisitApp.GetDialysisCheckImage(records));
        }

    }
}
