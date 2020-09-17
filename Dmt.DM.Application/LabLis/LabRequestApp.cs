using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Code.Excel;
using Dmt.DM.Domain.Entity.LabLis;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.EntityFrameWork;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Dmt.DM.Application.LabLis
{
    public interface ILabRequestApp : IScopedAppService
    {
        /// <summary>
        /// 根据申请日期和申请单状态查询
        /// </summary>
        /// <param name="requestDate">申请日期</param>
        /// <param name="requestStatus">申请单状态</param>
        /// <returns></returns>
        Task<List<LabSheetEntity>> GetList(DateTime requestDate, int? requestStatus);

        Task<List<LabSheetItemsEntity>> GetItemsList(DateTime requestDate, List<long> requestIds);

        /// <summary>
        /// 查询单一明细记录
        /// </summary>
        /// <param name="requestDate"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<List<LabSheetItemsEntity>> GetItem(DateTime requestDate, long requestId);

        /// <summary>
        /// 批量写入数据
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="itemids"></param>
        Task CreateRecords(string pids, string itemids);

        /// <summary>
        /// 生成并返回Excel文件名
        /// </summary>
        /// <returns></returns>
        Task<string> CopyModelFile();

        /// <summary>
        /// 生成Excel表格
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="filepath"></param>
        /// <param name="hospitalName"></param>
        Task FillDataAsync(string ids, string filepath, string hospitalName);

        /// <summary>
        /// 通过条码号查找申请单
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        Task<List<LabSheetEntity>> GetFormByBarcode(string barcode);

        Task BatchCreateBarcode(string ids);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        Task BatchDelete(string ids);

        Task<string> SampleSpit(string keyValue, string items);
        Task<string> SampleMerge(string ids);
        Task BatchSign(DateTime requestDate);

        /// <summary>
        /// 根据患者ID查询近期化验记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<LabSheetEntity>> GetListByPid(string keyword);

        Task<LabSheetEntity> GetForm(string keyValue);
        Task DeleteForm(string keyValue);
        Task SignForm(string keyValue);
        Task<int> UpdateForm(LabSheetEntity entity);
        Task SubmitForm(LabSheetEntity entity, string keyValue);
    }

    public class LabRequestApp : ILabRequestApp
    {
        private readonly IRepository<LabSheetEntity> _service = null;
        private readonly IRepository<LabSheetItemsEntity> _detailService = null;
        private readonly ILabItemApp _labItemApp = null;
        private readonly IPatientApp _patientApp = null;
        private readonly IUnitOfWork<HsDbContext> _uow = null;
        private readonly IUsersService _usersService = null;

        public LabRequestApp(IUnitOfWork<HsDbContext> uow, IUsersService usersService, ILabItemApp labItemApp,IPatientApp patientApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<LabSheetEntity>();
            _detailService = _uow.GetRepository<LabSheetItemsEntity>();
            _usersService = usersService;
            _labItemApp = labItemApp;
            _patientApp = patientApp;
        }

        /// <summary>
        /// 根据申请日期和申请单状态查询
        /// </summary>
        /// <param name="requestDate">申请日期</param>
        /// <param name="requestStatus">申请单状态</param>
        /// <returns></returns>
        public Task<List<LabSheetEntity>> GetList(DateTime requestDate, int? requestStatus)
        {
            var expression = ExtLinq.True<LabSheetEntity>();
            expression = expression.And(t => t.F_RequestDate == requestDate);
            if (requestStatus.HasValue)
            {
                var status = requestStatus.ToInt();
                expression = expression.And(t => t.F_Status == status);
            }

            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        /// <summary>
        /// 查询明细记录
        /// </summary>
        /// <param name="requestDate"></param>
        /// <param name="requestIds"></param>
        /// <returns></returns>
        public Task<List<LabSheetItemsEntity>> GetItemsList(DateTime requestDate, List<long> requestIds)
        {
            var expression = ExtLinq.True<LabSheetItemsEntity>();
            expression = expression.And(t => t.F_RequestDate == requestDate);
            expression = expression.And(t => t.F_DeleteMark != true);
            var list = _detailService.IQueryable(expression);
            return list.Where(t => requestIds.Contains(t.F_RequestId)).ToListAsync();
        }

        /// <summary>
        /// 查询单一明细记录
        /// </summary>
        /// <param name="requestDate"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public Task<List<LabSheetItemsEntity>> GetItem(DateTime requestDate, long requestId)
        {
            var expression = ExtLinq.True<LabSheetItemsEntity>();
            expression = expression.And(t => t.F_RequestDate == requestDate);
            expression = expression.And(t => t.F_RequestId == requestId);
            return _detailService.IQueryable(expression).ToListAsync();
        }

        private Task<string> CreateBarcode(long requestId)
        {
            string barcode;
            var parameterin = new MySqlParameter("@requestId", requestId)
            {
                Direction = ParameterDirection.Input
            };
            var parameterout = new MySqlParameter("@barcodeID", MySqlDbType.VarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            try
            {
                _uow.DbContext.Database.ExecuteSqlCommand(new RawSqlString("CreateBarcode @requestId, @barcodeID OUTPUT"),
                    new object[] {parameterin, parameterout});
                //DbHelper.RunProcedure("CreateBarcode", new SqlParameter[] { parameterin, parameterout }, out int i);
                barcode = parameterout.Value + "";
            }
            catch
            {
                return Task.FromResult("");
            }
           
            return Task.FromResult(barcode);
        }

        /// <summary>
        /// 批量写入数据
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="itemids"></param>
        public async Task CreateRecords(string pids, string itemids)
        {
            //var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            //claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var user = await _usersService.GetCurrentUserAsync();
            var pidsParam = pids.ToJArrayObject();
            var itemidsParam = itemids.ToJArrayObject();
            var labItemList = new List<LabItemEntity>();
            foreach (var item in itemidsParam)
            {
                var itemId = item.Value<string>("id");
                var entity = await _labItemApp.GetForm(itemId);
                if (entity != null)
                {
                    labItemList.Add(entity);
                }
            }
            //var user = OperatorProvider.Provider.GetCurrent();

            var patients = new List<PatientEntity>();
            foreach (var group in labItemList.GroupBy(t => t.F_CuvetteNo))
            {
                foreach (var item in pidsParam)
                {
                    var pId = item.Value<string>("id");
                    var patient = patients.FirstOrDefault(t => t.F_Id == pId);
                    if (patient == null)
                    {
                        patient = await _patientApp.GetForm(pId);
                        if (patient == null) continue;
                        patients.Add(patient);
                    }

                    //var claimUserId = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier));
                    //var claimUserName = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.Name));
                    var masterEntity = new LabSheetEntity
                    {
                        F_Id = Common.GuId(),
                        F_CreatorTime = DateTime.Now,
                        F_CreatorUserId = user?.F_Id,
                        F_RequestDate = DateTime.Today,
                        F_RequestId = long.Parse(Common.CreateNo()),
                        F_Pid = patient.F_Id,
                        F_Name = patient.F_Name,
                        F_BeInfected = "+".Equals(patient.F_Tp) || "+".Equals(patient.F_Hiv) || "+".Equals(patient.F_HBsAg) || "+".Equals(patient.F_HBeAg) || "+".Equals(patient.F_HBeAb),
                        F_DialysisNo = patient.F_DialysisNo,
                        F_RecordNo = patient.F_RecordNo,
                        F_PatientNo = patient.F_RecordNo,
                        F_Gender = patient.F_Gender,
                        F_BirthDay = patient.F_BirthDay,
                        F_InsuranceNo = patient.F_InsuranceNo,
                        F_IdNo = patient.F_IdNo,
                        F_MaritalStatus = patient.F_MaritalStatus,
                        F_IdealWeight = patient.F_IdealWeight,
                        F_Height = patient.F_Height,
                        F_PrimaryDisease = patient.F_PrimaryDisease,
                        F_Diagnosis = patient.F_Diagnosis,
                        F_SampleType = group.First().F_SampleType,
                        F_Container = group.First().F_Container,
                        F_Status = 1,
                        F_DoctorId = user?.F_Id,
                        F_DoctorName = user?.F_RealName,
                        F_OrderTime = DateTime.Now
                    };
                    await _service.InsertAsync(masterEntity);
                    var details = new List<LabSheetItemsEntity>();
                    foreach (var child in group)
                    {
                        details.Add(new LabSheetItemsEntity
                        {
                            F_Id = Common.GuId(),
                            F_CreatorTime = masterEntity.F_CreatorTime,
                            F_CreatorUserId = user?.F_Id,
                            F_RequestId = masterEntity.F_RequestId,
                            F_Code = child.F_Code,
                            F_Name = child.F_Name,
                            F_EnName = child.F_EnName,
                            F_ShortName = child.F_ShortName,
                            F_SampleType = child.F_SampleType,
                            F_Container = child.F_Container,
                            F_CuvetteNo = child.F_CuvetteNo,
                            F_Type = child.F_Type,
                            F_ThirdPartyCode = child.F_ThirdPartyCode,
                            F_IsExternal = child.F_IsExternal,
                            F_Sorter = child.F_Sorter,
                            F_IsPeriodic = child.F_IsPeriodic,
                            F_TimeInterval = child.F_TimeInterval
                        });
                    }
                    await _detailService.InsertAsync(details);
                }
            }
        }

        /// <summary>
        /// 生成并返回Excel文件名
        /// </summary>
        /// <returns></returns>
        public Task<string> CopyModelFile()
        {
            var targetFileName = "\\DataReport\\" + Common.CreateNo() + ".xls";
            return Task.FromResult(targetFileName);
        }

        /// <summary>
        /// 生成Excel表格
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="filepath"></param>
        /// <param name="hospitalName"></param>
        public async Task FillDataAsync(string ids, string filepath, string hospitalName)
        {
            var excel = new NPOIExcel();
            var masterList = new List<LabSheetEntity>();
            var detailList = new List<LabSheetItemsEntity>();
            foreach (var item in ids.Split(','))
            {
                var id = item;
                var entity = await _service.FindEntityAsync(id);
                if (entity == null || !entity.F_SamplingTime.HasValue) continue;
                var child = await GetItem(entity.F_RequestDate, entity.F_RequestId);
                if (child.Count == 0) continue;
                masterList.Add(entity);
                detailList.AddRange(child);
            }
            var table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("申请单号",typeof(long)),
                new DataColumn("姓名"),
                new DataColumn("性别"),
                new DataColumn("出生日期",typeof(DateTime)),
                new DataColumn("透析号",typeof(int)),
                new DataColumn("病历号"),
                new DataColumn("条码号"),
                new DataColumn("检验项目"),
                new DataColumn("项目代码"),
                new DataColumn("采集时间",typeof(DateTime)),
                new DataColumn("标本类型"),
                new DataColumn("采集容器")
            });

            var data = masterList.GroupJoin(detailList, m => m.F_RequestId, d => d.F_RequestId, (m, d) => new
            {
                m.F_RequestId,
                m.F_Name,
                m.F_Gender,
                m.F_BirthDay,
                m.F_DialysisNo,
                m.F_RecordNo,
                m.F_Barcode,
                m.F_SamplingTime,
                m.F_SampleType,
                m.F_Container,
                rows = d.Select(c => new
                {
                    c.F_Code,
                    c.F_Name,
                    c.F_ThirdPartyCode,
                    c.F_ShortName,
                    c.F_Sorter
                }).OrderBy(c => c.F_Sorter)
            }).OrderBy(n => n.F_Name).ThenBy(n => n.F_SamplingTime);

            foreach (var item in data)
            {
                var itemDesc = string.Join(",", item.rows.Select(t => t.F_ShortName));
                var codeDesc = string.Join(",", item.rows.Select(t => t.F_ThirdPartyCode));
                DataRow dr = table.NewRow();
                dr.ItemArray = new object[] {
                    item.F_RequestId,
                    item.F_Name,
                    item.F_Gender,
                    item.F_BirthDay,
                    item.F_DialysisNo,
                    item.F_RecordNo,
                    item.F_Barcode,
                    itemDesc,
                    codeDesc,
                    item.F_SamplingTime,
                    item.F_SampleType,
                    item.F_Container
                };
                table.Rows.Add(dr);
            }
            excel.ToExcel(table, hospitalName + " 采样表 ", "采样明细", filepath);
        }

        /// <summary>
        /// 通过条码号查找申请单
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public Task<List<LabSheetEntity>> GetFormByBarcode(string barcode)
        {
            var expression = ExtLinq.True<LabSheetEntity>();
            expression = expression.And(t => t.F_Barcode == barcode);
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public async Task BatchCreateBarcode(string ids)
        {
            var json = ids.ToJArrayObject();
            foreach (var id in json.Select(item => item.Value<string>("id")))
            {
                var entity = await _service.FindEntityAsync(id);
                if (entity == null) continue;
                if (!entity.F_Barcode.IsEmpty()) continue;
                var str = await CreateBarcode(entity.F_RequestId);
                if (str.IsEmpty()) continue;
                entity.F_Barcode = str;
                await _service.UpdatePartialAsync(entity);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        public async Task BatchDelete(string ids)
        {
            var json = ids.ToJArrayObject();
            foreach (var item in json)
            {
                var id = item.Value<string>("id");
                var entity = await _service.FindEntityAsync(id);
                if (entity != null)
                {
                    await _detailService.DeleteAsync(t => t.F_RequestId == entity.F_RequestId);
                    await _service.DeleteAsync(entity);
                }
            }
        }

        public async Task<string> SampleSpit(string keyValue, string items)
        {
            var entity = await _service.FindEntityAsync(keyValue);
            var details = await GetItem(entity.F_RequestDate, entity.F_RequestId);
            var modifyRows = new List<LabSheetItemsEntity>();
            foreach (var item in items.ToJArrayObject())
            {
                var id = item.Value<string>("id");
                var find = details.FirstOrDefault(t => t.F_Id == id);
                if (find == null) continue;
                modifyRows.Add(find);
            }
            if (modifyRows.Count == 0) return "";
            var newsheet = new LabSheetEntity
            {
                F_Id = Common.GuId(),
                F_CreatorTime = DateTime.Now,
                F_CreatorUserId = entity.F_CreatorUserId,
                F_RequestDate = entity.F_RequestDate,
                F_RequestId = long.Parse(Common.CreateNo()), //生成新的申请单号
                F_Pid = entity.F_Pid,
                F_Name = entity.F_Name,
                F_DialysisNo = entity.F_DialysisNo,
                F_RecordNo = entity.F_RecordNo,
                F_PatientNo = entity.F_RecordNo,
                F_Gender = entity.F_Gender,
                F_BirthDay = entity.F_BirthDay,
                F_InsuranceNo = entity.F_InsuranceNo,
                F_IdNo = entity.F_IdNo,
                F_MaritalStatus = entity.F_MaritalStatus,
                F_IdealWeight = entity.F_IdealWeight,
                F_Height = entity.F_Height,
                F_PrimaryDisease = entity.F_PrimaryDisease,
                F_Diagnosis = entity.F_Diagnosis,
                F_SampleType = entity.F_SampleType,
                F_Container = entity.F_Container,
                F_Status = entity.F_Status,
                F_DoctorId = entity.F_DoctorId,
                F_DoctorName = entity.F_DoctorName,
                F_OrderTime = entity.F_OrderTime
            };
            await _service.InsertAsync(newsheet);
            foreach (var item in modifyRows)
            {
                item.F_RequestId = newsheet.F_RequestId;
                await _detailService.UpdateAsync(item, true);
            }
            return newsheet.F_Id;
        }

        public async Task<string> SampleMerge(string ids)
        {
            var jsonObject = ids.ToJArrayObject();
            var requestDate = DateTime.Today;
            long requestId = 0;
            var id = string.Empty;
            for (var i = 0; i < jsonObject.Count; i++)
            {
                if (i == 0) //first 
                {
                    id = jsonObject[i].Value<string>("id");
                    var entity = await _service.FindEntityAsync(jsonObject[i].Value<string>("id"));
                    requestDate = entity.F_RequestDate;
                    requestId = entity.F_RequestId;
                }
                else
                {
                    var entity = await _service.FindEntityAsync(jsonObject[i].Value<string>("id"));
                    var details = await GetItem(entity.F_RequestDate, entity.F_RequestId);
                    for (var j = 0; j < details.Count; j++)
                    {
                        details[j].F_RequestDate = requestDate;
                        details[j].F_RequestId = requestId;
                        _detailService.Update(details[j]);
                    }
                    await _service.DeleteAsync(entity);
                }
            }
            return id;
        }

        public async Task BatchSign(DateTime requestDate)
        {
            var expression = ExtLinq.True<LabSheetEntity>();
            expression = expression.And(t => t.F_RequestDate == requestDate && t.F_Status == 1);
            expression = expression.And(t => t.F_DeleteMark != true);
            foreach (var item in _service.IQueryable(expression))
            {
                item.F_Status = 2;
                await _service.UpdatePartialAsync(item);
            }
        }

        /// <summary>
        /// 根据患者ID查询近期化验记录
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public Task<List<LabSheetEntity>> GetListByPid(string keyword)
        {
            var expression = ExtLinq.True<LabSheetEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Pid == keyword);
            }
            else
            {
                expression = expression.And(t => t.F_Pid == "#");
            }

            expression = expression.And(t => t.F_EnabledMark == true);
            return  _service.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).Take(20).ToListAsync();
            //var list = _service.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).Take(20).ToList();
            ////查询开单明细
            //var ids = list.Select(t => t.F_RequestId).ToList();
            //var detailExpression = ExtLinq.True<LabSheetItemsEntity>();
            //detailExpression = detailExpression.And(t => ids.Contains(t.F_RequestId));
            //detailExpression = detailExpression.And(t => t.F_DeleteMark != true);
            //var detailItems = _detailService.IQueryable(detailExpression);
            ////foreach (var item in list)
            ////{
            ////    item.Items = detailItems.Where(t => t.F_RequestId == item.F_Id)).ToList();
            ////}
            //return list;
        }

        public Task<LabSheetEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
            //var entity = _service.FindEntity(keyValue);
            //var detailExpression = ExtLinq.True<LabSheetItemsEntity>();
            //detailExpression = detailExpression.And(t => t.F_RequestId == entity.F_RequestId));
            //detailExpression = detailExpression.And(t => t.F_DeleteMark != true);
            //return entity;
        }
        public async Task DeleteForm(string keyValue)
        {
            var entity = await _service.FindEntityAsync(keyValue);
            await _detailService.DeleteAsync(t => t.F_RequestId == entity.F_RequestId);
            await _service.DeleteAsync(entity);
            //
            //entity.F_DeleteMark = true;
            //entity.F_EnabledMark = false;
            //UpdateForm(entity, true); 
        }

        public async Task SignForm(string keyValue)
        {
            var entity =await _service.FindEntityAsync(keyValue);
            if (entity.F_Status == 1)
            {
                entity.F_Status = 2;
                await UpdateForm(entity);
            }
        }

        public Task<int> UpdateForm(LabSheetEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public async Task SubmitForm(LabSheetEntity entity, string keyValue)
        {
            //var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            //claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            //var claim = claimsIdentity?.FindFirst(t => t.Type == ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                await _service.UpdatePartialAsync(entity);
                //删除明细
                await _detailService.DeleteAsync(t => t.F_RequestId.ToString() == keyValue);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                await _service.InsertAsync(entity);
            }
            //循环写入明细
            //foreach (var item in entity.Items)
            //{
            //    item.Create();
            //    _detailService.Insert(item);
            //}
        }

    }
}
