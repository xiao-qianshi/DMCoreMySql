using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.JSReporting;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;

namespace Dmt.DM.Application.DataReport
{
    public interface IDataReportJSApp :IScopedAppService
    {
        /// <summary>
        /// 根据月份筛选
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<List<DataReportJSEntity>> GetList(string month);

        Task<DataReportJSEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DataReportJSEntity entity, bool isPartial = false);
        Task<int> SubmitForm(DataReportJSEntity entity, string keyValue);

        /// <summary>
        /// 创建上报文件
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="patientId">患者Id</param>
        XmlDocument CreateRecord(string month, string patientId);

        XmlDocument CombineXmlFile(string ids, string rootPath);
        void SubmitData(string patientId, string month, string fileName, string filePath, string userId);
    }

    public class DataReportJSApp : IDataReportJSApp
    {
        private readonly IRepository<DataReportJSEntity> _service = null;
        private IUnitOfWork _uow = null;
        private IHttpContextAccessor _httpContext = null;

        public DataReportJSApp(IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _service = _uow.GetRepository<DataReportJSEntity>();
            _httpContext = httpContext;
        }

        /// <summary>
        /// 根据月份筛选
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public Task<List<DataReportJSEntity>> GetList(string month)
        {
            var expression = ExtLinq.True<DataReportJSEntity>();
            expression = expression.And(t => t.F_MonthDesc==month);
            return _service.IQueryable(expression).ToListAsync();
        }

        public Task<DataReportJSEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id==keyValue);
        }

        public Task<int> UpdateForm(DataReportJSEntity entity, bool isPartial = false)
        {
            return _service.UpdateAsync(entity, isPartial);
        }

        public Task<int> SubmitForm(DataReportJSEntity entity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                //entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(entity);
            }
        }
        /// <summary>
        /// 创建上报文件
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="patientId">患者Id</param>
        public XmlDocument CreateRecord(string month, string patientId)
        {
            var startDate = DateTime.Parse(month + "-01"); //当月1号
            var endDate = startDate.AddMonths(1).AddDays(-1); //当月最后一天
            var endDate2 = startDate.AddMonths(1); //下月1号
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xdec = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(xdec);
            XmlElement xroot = xdoc.CreateElement("patientList");
            xdoc.AppendChild(xroot);
            var patient = _uow.GetRepository<PatientEntity>().FindEntity(patientId);
            var patientNode = xdoc.CreateElement("patient");
            //1 患者基本信息
            xroot.AppendChild(patientNode);
            patientNode.SetAttribute("id", patient.F_Id);
            var node = xdoc.CreateElement("patientCaseNo");
            node.SetAttribute("caseNumber", patient.F_DialysisNo + "");
            patientNode.AppendChild(node);
            //1.1 患者基本信息
            node = xdoc.CreateElement("basicInfo");
            patientNode.AppendChild(node);
            var child = xdoc.CreateElement("name");
            child.InnerText = patient.F_Name;
            node.AppendChild(child);
            child = xdoc.CreateElement("sex");
            node.AppendChild(child);
            var child2 = xdoc.CreateElement("value");
            child2.InnerText = patient.F_Gender;
            child.AppendChild(child2);
            child = xdoc.CreateElement("birthDay");
            child.InnerText = patient.F_BirthDay.HasValue ? patient.F_BirthDay.ToDateString() : "";
            node.AppendChild(child);
            child = xdoc.CreateElement("identityID");
            child.InnerText = patient.F_IdNo ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("address");
            child.InnerText = patient.F_Address ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("currentAddress");
            child.InnerText = patient.F_Address ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("tel");
            child.InnerText = patient.F_PhoneNo ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("mobileTel");
            child.InnerText = patient.F_PhoneNo2 ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("relative");
            //child.InnerText = patient.F_Contacts ?? "";
            node.AppendChild(child);
            child = xdoc.CreateElement("relationship");
            node.AppendChild(child);
            child = xdoc.CreateElement("relativeTel");
            node.AppendChild(child);
            child = xdoc.CreateElement("relativeMobile");
            node.AppendChild(child);
            child = xdoc.CreateElement("zipCode");
            node.AppendChild(child);
            child = xdoc.CreateElement("inputDate");
            node.AppendChild(child);
            child.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
            child = xdoc.CreateElement("markName");
            node.AppendChild(child);
            //1.2 患者基本信息选择项
            node = xdoc.CreateElement("userSections");
            patientNode.AppendChild(node);
            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_FDD");
            child.SetAttribute("name", "首次诊治日期");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_DialysisStartTime.HasValue ? patient.F_DialysisStartTime.ToDate().ToDateString() : "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_FD");
            child.SetAttribute("name", "首次透析日期");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_DialysisStartTime.HasValue ? patient.F_DialysisStartTime.ToDate().ToDateString() : "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_DA");
            child.SetAttribute("name", "透析龄（月）");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_DialysisStartTime.HasValue ? ((DateTime.Today - patient.F_DialysisStartTime.ToDate()).TotalDays / 30).ToInt() + "" : "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_marrige");
            child.SetAttribute("name", "婚姻状况");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_MaritalStatus ?? "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_career");
            child.SetAttribute("name", "职业");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            //child2.InnerText = "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U0003");
            child.SetAttribute("name", "病人联系人");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_Contacts ?? "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U0004");
            child.SetAttribute("name", "联系人与病人关系");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            //child2.InnerText = patient.F_Contacts ?? "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_CT");
            child.SetAttribute("name", "费别");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);

            var insuranceType = patient.F_InsuranceType ?? "";
            if (!insuranceType.IsEmpty())
            {
                switch (insuranceType)
                {
                    case "职工医保":
                        insuranceType = "城镇职工医疗保险";
                        break;
                    case "居民医保":
                        insuranceType = "居民医疗保险";
                        break;
                    case "省医保":
                        insuranceType = "城镇职工医疗保险";
                        break;
                    case "农合":
                        insuranceType = "其它";
                        break;
                    case "自费":
                        insuranceType = "自费医疗";
                        break;
                    default:
                        insuranceType = "";
                        break;
                }
            }
            child2.InnerText = insuranceType;

            //< option value = "职工医保" > 职工医保 </ option >
            //< option value = "居民医保" > 居民医保 </ option >
            //< option value = "省医保" > 省医保 </ option >
            //< option value = "农合" > 农合 </ option >
            //< option value = "自费" > 自费 </ option >
            //0:城镇职工医疗保险,1:居民医疗保险,2:自费医疗,3:公费医疗,7:学生医疗保险,4:商业保险,6:其它							
            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U0005");
            child.SetAttribute("name", "联系人电话");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            //child2.InnerText = patient.F_Contacts ?? "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "UCID1");
            child.SetAttribute("name", "透析病案号");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            child2.InnerText = patient.F_RecordNo ?? "";

            child = xdoc.CreateElement("item");
            node.AppendChild(child);
            child.SetAttribute("id", "U_address_city");
            child.SetAttribute("name", "所在城市");
            child2 = xdoc.CreateElement("value");
            child.AppendChild(child2);
            //child2.InnerText = patient.F_RecordNo ?? "";

            //1.3 患者表单信息
            node = xdoc.CreateElement("itemInfor");
            patientNode.AppendChild(node);
            //1.3.1 globalItem 
            child = xdoc.CreateElement("globalItem");
            node.AppendChild(child);
            //1.3.2 commonItem
            var commonItemNode = xdoc.CreateElement("commonItem");
            node.AppendChild(commonItemNode);

            //用药处方
            //查询本月药品处方执行记录
            var orderList = _uow.GetRepository<OrdersExecLogEntity>().IQueryable()
                .Where(t => t.F_Pid==patient.F_Id && t.F_NurseOperatorTime >= startDate && t.F_NurseOperatorTime <= endDate2 && t.F_DeleteMark != true && t.F_OrderType.Equals("药疗"))
                .Select(t => new
                {
                    t.F_Oid,
                    t.F_OrderCode,
                    t.F_OrderText,
                    t.F_OrderSpec,
                    t.F_OrderUnitAmount,
                    t.F_OrderUnitSpec,
                    t.F_OrderAmount,
                    t.F_OrderFrequency,
                    t.F_OrderAdministration,
                    t.F_IsTemporary,
                    t.F_Doctor,
                    t.F_DoctorOrderTime,
                    t.F_DoctorAuditTime,
                    t.F_Nurse,
                    t.F_NurseId,
                    t.F_NurseOperatorTime,
                    t.F_Memo
                })
                .Join(_uow.GetRepository<DrugsEntity>().IQueryable().Where(t => t.F_DrugEfficacy != null), m => m.F_OrderCode, n => n.F_Id, (m, n) => new
                {
                    m.F_Oid,
                    m.F_OrderCode,
                    m.F_OrderText,
                    m.F_OrderSpec,
                    m.F_OrderUnitAmount,
                    m.F_OrderUnitSpec,
                    m.F_OrderAmount,
                    m.F_OrderFrequency,
                    m.F_OrderAdministration,
                    m.F_IsTemporary,
                    m.F_Doctor,
                    m.F_DoctorOrderTime,
                    m.F_DoctorAuditTime,
                    m.F_Nurse,
                    m.F_NurseId,
                    m.F_NurseOperatorTime,
                    m.F_Memo,
                    n.F_DrugAdministration,
                    n.F_DrugEfficacy,
                    n.F_DrugMiniAmount,
                    n.F_DrugMiniPackage,
                    n.F_DrugMiniSpec,
                    n.F_DrugName,
                    n.F_DrugSpec,
                    n.F_DrugUnit,
                    n.F_IsHeparin
                })
                .ToList();

            //{ id: "抗凝剂", text: "抗凝剂" },
            //{ id: "促红素", text: "促红素" },
            //{ id: "铁剂(口服)", text: "铁剂(口服)" },
            //{ id: "铁剂(注射)", text: "铁剂(注射)" },
            //{ id: "抗高血压药", text: "抗高血压药" },
            //{ id: "营养药物(口服)", text: "营养药物(口服)" },
            //{ id: "营养药物(注射)", text: "营养药物(注射)" },
            //{ id: "降脂药物", text: "降脂药物" },
            //{ id: "MBD干预药(口服)", text: "MBD干预药(口服)" },
            //{ id: "MBD干预药(注射)", text: "MBD干预药(注射)" },
            //{ id: "降脂药物", text: "降脂药物" },
            //{ id: "活性维生素D", text: "活性维生素D" },
            //{ id: "钙剂", text: "钙剂" },
            //{ id: "降磷药物", text: "降磷药物" },
            //{ id: "其他", text: "其他" }

            //营养支持药物
            var filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("营养药物(口服)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "YY_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "营养支持药物（口服）");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "YY_list_K");
                itemNode.SetAttribute("name", "营养支持药物处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"YY_list_K_01\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"YY_list_K_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"YY_list_K_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"YY_list_K_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"YY_list_K_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"YY_list_K_F1\",\"itemValue\":\"" + "其他"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"YY_list_K_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"YY_list_K_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"YY_list_K_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //降脂药物
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("降脂药物")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "JZ_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "降脂药物");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "JZ_list_K");
                itemNode.SetAttribute("name", "降脂药物处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"JZ_list_K_01\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"JZ_list_K_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"JZ_list_K_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"JZ_list_K_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"JZ_list_K_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"JZ_list_K_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"JZ_list_K_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"JZ_list_K_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //抗血小板药物
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("抗血小板药物")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "KX_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "抗血小板药物");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "KX_list_K");
                itemNode.SetAttribute("name", "抗血小板药物处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"KX_list_K_01\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"KX_list_K_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"KX_list_K_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"KX_list_K_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"KX_list_K_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"KX_list_K_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"KX_list_K_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"KX_list_K_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //MBD干预药（注射）
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("MBD干预药(注射)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "MBD");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "MBD干预药（注射）");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "MBD_list");
                itemNode.SetAttribute("name", "MBD干预药处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"MBD_list_01\",\"itemValue\":\"" + "维生素D及衍生物"
                    + "\"},{\"itemId\":\"MBD_list_02\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"MBD_list_03\",\"itemValue\":\"" + ""
                    + "\"},{\"itemId\":\"MBD_list_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"MBD_list_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"MBD_list_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"MBD_list_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"MBD_list_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"MBD_list_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"MBD_list_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //营养支持药物（静脉/注射）
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("营养药物(注射)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "YY");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "营养支持药物（静脉/注射）");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "YY_list");
                itemNode.SetAttribute("name", "营养支持药物处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"YY_list_01\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"YY_list_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"YY_list_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"YY_list_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"YY_list_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"YY_list_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"YY_list_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"YY_list_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //MBD干预药（口服）
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("MBD干预药(口服)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "MBD_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "MBD干预药（口服）");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "YY_list");
                itemNode.SetAttribute("name", "MBD干预药处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"MBD_list_K_01\",\"itemValue\":\"" + "维生素D及衍生物"
                    + "\"},{\"itemId\":\"MBD_list_K_02\",\"itemValue\":\"" + r.item.F_DrugName
                    + "\"},{\"itemId\":\"MBD_list_K_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"MBD_list_K_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"MBD_list_K_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"MBD_list_K_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"MBD_list_K_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"MBD_list_K_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"MBD_list_K_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //抗高血压药【新】
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("抗高血压药")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "KG_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "抗高血压药【新】");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "KG_list_K");
                itemNode.SetAttribute("name", "抗高血压药处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"KG_list_K_01\",\"itemValue\":\"" + "钙通道阻滞剂"
                    + "\"},{\"itemId\":\"KG_list_K_medical\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"KG_list_K_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"KG_list_K_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"KG_list_K_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"KG_list_K_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"KG_list_K_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"KG_list_K_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //铁剂（静脉）
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("铁剂(注射)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "TJ");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "铁剂（静脉）");
                var findrow = filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault();

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_01");
                itemNode.SetAttribute("name", "铁剂种类");
                itemNode.SetAttribute("type", "44");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName ?? "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_medical");
                itemNode.SetAttribute("name", "药品名称");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName + "(" + findrow.F_DrugMiniSpec + ")";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_02");
                itemNode.SetAttribute("name", "方式");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "静脉";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_format");
                itemNode.SetAttribute("name", "剂量");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_OrderAmount + "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_F");
                itemNode.SetAttribute("name", "用药频次");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = (findrow.F_OrderFrequency ?? "").ToLower();

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_details");
                itemNode.SetAttribute("name", "铁剂（静脉）医嘱描述");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName + "(" + findrow.F_DrugMiniSpec + ")" + " " + findrow.F_OrderAmount + " " + (findrow.F_OrderFrequency ?? "").ToLower();

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_doctor");
                itemNode.SetAttribute("name", "处方医生");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = (findrow.F_Doctor ?? "").ToLower();

                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //促红素
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("促红素")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "CHS");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "促红素");
                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "CHS_list");
                itemNode.SetAttribute("name", "促红素处方");
                itemNode.SetAttribute("type", "24");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = filterOrders.Count + "";
                tempNode = xdoc.CreateElement("arrayStr");
                //处方明细信息
                var rows = filterOrders.GroupBy(t => t.F_OrderCode, (key, row) => new
                {
                    item = row.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault()
                }).Select(r => new
                {
                    childList = "[{\"itemId\":\"CHS_list_01\",\"itemValue\":\"" + "国产"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"CHS_list_02\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")"
                    + "\",\"labValue\":\"\"},{\"itemId\":\"CHS_list_04\",\"itemValue\":\"" + ((r.item.F_OrderAdministration ?? "").Contains("皮下") ? "皮下" : "静脉")
                    + "\",\"labValue\":\"\"},{\"itemId\":\"CHS_list_format\",\"itemValue\":\"" + r.item.F_OrderAmount
                    + "\"},{\"itemId\":\"CHS_list_F\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\",\"labValue\":\"\"},{\"itemId\":\"CHS_list_FC\",\"itemValue\":\"" + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"CHS_list_FDS\",\"itemValue\":\"" +
                    (
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Monday ? "周一" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Tuesday ? "周二" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Wednesday ? "周三" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Thursday ? "周四" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Friday ? "周五" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Saturday ? "周六" :
                        r.item.F_NurseOperatorTime.ToDate().Date.DayOfWeek == DayOfWeek.Sunday ? "周日" :
                            "周一"
                    )
                    + "\",\"labValue\":\"\"},{\"itemId\":\"CHS_list_details\",\"itemValue\":\"" + r.item.F_DrugName + "(" + r.item.F_DrugMiniSpec + ")" + " " + r.item.F_OrderAmount + " " + (r.item.F_OrderFrequency ?? "").ToLower()
                    + "\"},{\"itemId\":\"CHS_list_doctor\",\"itemValue\":\"" + (r.item.F_Doctor ?? "")
                    + "\"},{\"itemId\":\"CHS_list_unique\",\"itemValue\":\"" + r.item.F_Oid
                    + "\"}]"
                });

                itemNode.AppendChild(tempNode);
                tempNode.InnerText = rows.ToJson();
                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }
            //铁剂（口服）
            filterOrders = orderList.Where(t => t.F_DrugEfficacy.Equals("铁剂(口服)")).ToList();
            if (filterOrders.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "TJ_K");
                sectionsNode.SetAttribute("actualExamineDate", filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault().F_NurseOperatorTime.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "铁剂（口服）");
                var findrow = filterOrders.OrderByDescending(t => t.F_NurseOperatorTime).FirstOrDefault();

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_01");
                itemNode.SetAttribute("name", "铁剂种类");
                itemNode.SetAttribute("type", "44");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName ?? "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_medical");
                itemNode.SetAttribute("name", "药品名称");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName + "(" + findrow.F_DrugMiniSpec + ")";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_02");
                itemNode.SetAttribute("name", "方式");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "口服";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_format");
                itemNode.SetAttribute("name", "剂量（mg）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_OrderAmount + "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_F");
                itemNode.SetAttribute("name", "用药频次");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = (findrow.F_OrderFrequency ?? "").ToLower();

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_FC");
                itemNode.SetAttribute("name", "用药频次代码");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = (findrow.F_OrderFrequency ?? "").ToLower();

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_details");
                itemNode.SetAttribute("name", "铁剂(口服)医嘱描述");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_DrugName + "(" + findrow.F_DrugMiniSpec + ")" + " " + findrow.F_OrderAmount + " " + (findrow.F_OrderFrequency ?? "").ToLower();

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "TJ_K_doctor");
                itemNode.SetAttribute("name", "处方医生");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = (findrow.F_Doctor ?? "").ToLower();

                tempNode = xdoc.CreateElement("createTimes");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = DateTime.Now.ToUnixTimeStamp() + "";
                tempNode = xdoc.CreateElement("editUserId");
                itemNode.AppendChild(tempNode);
            }

            //体重变化（质控）
            var weightRecord = _uow.GetRepository<PatVisitEntity>().IQueryable()
                .Where(t => t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_Pid==patientId && t.F_DialysisEndTime != null && t.F_WeightTQ != null && t.F_WeightSXTH != null)
                .Select(t => new
                {
                    t.F_WeightTQ,
                    t.F_WeightSXTH,
                    t.F_VisitDate
                })
                .OrderByDescending(t => t.F_VisitDate)
                .FirstOrDefault();
            if (weightRecord != null && weightRecord.F_WeightTQ.HasValue)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "QC_WeightChange");
                sectionsNode.SetAttribute("actualExamineDate", weightRecord.F_VisitDate.ToDate().ToDateTimeString().Substring(0, 16));
                sectionsNode.SetAttribute("sectionName", "体重变化（质控）");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "QC_180030003");
                itemNode.SetAttribute("name", "上次透后体重(KG)");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = weightRecord.F_WeightSXTH.ToFloat(2) + "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "QC_180030004");
                itemNode.SetAttribute("name", "透前体重(KG)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = weightRecord.F_WeightTQ.ToFloat(2) + "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "QC_1800300061");
                itemNode.SetAttribute("name", "体重变化率(%)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = ((weightRecord.F_WeightTQ.ToFloat(2) - weightRecord.F_WeightSXTH.ToFloat(2)) / weightRecord.F_WeightSXTH.ToFloat(2)).ToFloat(0) + "";
            }

            //查询化验结果
            var lisResultList = _uow.GetRepository<QualityResultEntity>().IQueryable()
                .Where(t => t.F_Pid==patientId && t.F_ReportTime > startDate && t.F_ReportTime < endDate2 && t.F_EnabledMark != false && t.F_DeleteMark != true)
                .Select(t => new
                {
                    t.F_Id,
                    t.F_ItemId,
                    t.F_ReportTime,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_Unit,
                    t.F_Result,
                    t.F_Flag,
                    t.F_LowerValue,
                    t.F_UpperValue,
                    t.F_ResultType
                })
                .GroupBy(t => t.F_ItemId, (key, rows) => new
                {
                    key,
                    item = rows.OrderByDescending(t => t.F_ReportTime).FirstOrDefault()
                })
                .ToList();
            //铁代谢
            var tempArr = new List<string>
                {
                    "FER",
                    "TS",
                    "Iron",
                    "TIBC"
                };
            var filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "51");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "铁代谢");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "482");
                itemNode.SetAttribute("name", "血清铁(umol/L)");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Iron")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                //需添加
                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10482");
                itemNode.SetAttribute("name", "转铁蛋白(ug/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("FER")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "483");
                itemNode.SetAttribute("name", "总铁结合力(umol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("TIBC")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                //需添加
                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "484");
                itemNode.SetAttribute("name", "转铁饱和度(%)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("eeeeeeeee")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "485");
                itemNode.SetAttribute("name", "铁蛋白(ug/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("FER")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

            }

            //透析状态
            var transferLog = _uow.GetRepository<TransferLogEntity>().IQueryable()
                .Where(t => t.F_Pid==patientId && t.F_TransferDate >= startDate && t.F_TransferDate <= endDate && t.F_EnabledMark != false)
                .OrderByDescending(t => t.F_TransferDate)
                .FirstOrDefault();
            if (transferLog != null)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "MMStatus");
                sectionsNode.SetAttribute("actualExamineDate", transferLog.F_TransferDate.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "透析状态");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "MMStatus_001");
                itemNode.SetAttribute("name", "患者状态");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //tempNode.InnerText = "在透"; /// 状态变更  转入,转出,退出,短期
                var transferStatus = transferLog.F_Status ?? "转入";
                tempNode.InnerText = transferStatus == "转入" || transferStatus == "转出" ? "在透" :
                    transferStatus == "短期" ? "短期" :
                    transferStatus == "退出" ? "转归" : "在透";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "G_Status_001");
                itemNode.SetAttribute("name", "状态变更");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = transferStatus;
            }

            //if (lastVisitDate == null || (endDate - lastVisitDate.ToDate()).TotalDays.ToInt() > 30)//30天内无透析记录 ，不在透析状态
            //{
            //    var sectionsNode = xdoc.CreateElement("sections");
            //    commonItemNode.AppendChild(sectionsNode);
            //    sectionsNode.SetAttribute("sectionID", "MMStatus");
            //    sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
            //    sectionsNode.SetAttribute("sectionName", "透析状态");

            //    var itemNode = xdoc.CreateElement("item");
            //    sectionsNode.AppendChild(itemNode);
            //    itemNode.SetAttribute("id", "MMStatus_001");
            //    itemNode.SetAttribute("name", "患者状态");
            //    itemNode.SetAttribute("type", "11");
            //    var tempNode = xdoc.CreateElement("value");
            //    itemNode.AppendChild(tempNode);
            //    tempNode.InnerText = "短期";

            //    itemNode = xdoc.CreateElement("item");
            //    sectionsNode.AppendChild(itemNode);
            //    itemNode.SetAttribute("id", "G_Status_001");
            //    itemNode.SetAttribute("name", "状态变更");
            //    itemNode.SetAttribute("type", "11");
            //    tempNode = xdoc.CreateElement("value");
            //    itemNode.AppendChild(tempNode);
            //    tempNode.InnerText = "转出";
            //}
            //else
            //{
            //    var sectionsNode = xdoc.CreateElement("sections");
            //    commonItemNode.AppendChild(sectionsNode);
            //    sectionsNode.SetAttribute("sectionID", "MMStatus");
            //    sectionsNode.SetAttribute("actualExamineDate", lastVisitDate.ToDate().ToDateTimeString().Substring(0, 10));
            //    sectionsNode.SetAttribute("sectionName", "透析状态");

            //    var itemNode = xdoc.CreateElement("item");
            //    sectionsNode.AppendChild(itemNode);
            //    itemNode.SetAttribute("id", "MMStatus_001");
            //    itemNode.SetAttribute("name", "患者状态");
            //    itemNode.SetAttribute("type", "11");
            //    var tempNode = xdoc.CreateElement("value");
            //    itemNode.AppendChild(tempNode);
            //    tempNode.InnerText = "在透";

            //    itemNode = xdoc.CreateElement("item");
            //    sectionsNode.AppendChild(itemNode);
            //    itemNode.SetAttribute("id", "G_Status_001");
            //    itemNode.SetAttribute("name", "状态变更");
            //    itemNode.SetAttribute("type", "11");
            //    tempNode = xdoc.CreateElement("value");
            //    itemNode.AppendChild(tempNode);
            //    tempNode.InnerText = "转入";
            //}

            //干体重 
            //查询干体重记录
            var ideaWeight = _uow.GetRepository<WeightLogEntity>().IQueryable()
                .Join(_uow.GetRepository<PatVisitEntity>().IQueryable().Where(t => t.F_Pid==patientId && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_DialysisEndTime != null)
                , a => a.F_Vid, b => b.F_Id,
                (a, b) =>
                new
                {
                    a.F_Value,
                    a.F_CreatorTime
                })
                .OrderByDescending(r => r.F_CreatorTime)
                .FirstOrDefault();
            if (ideaWeight != null && ideaWeight.F_Value.HasValue)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "44");
                sectionsNode.SetAttribute("actualExamineDate", ideaWeight.F_CreatorTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "干体重");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "471");
                itemNode.SetAttribute("name", "情况");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "非卧床";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "472");
                itemNode.SetAttribute("name", "干体重（kg）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = ideaWeight.F_Value.ToFloat(2) + "";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "472_0");
                itemNode.SetAttribute("name", "达标");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "是";
            }

            //原发病诊断  需要添加
            if (1 == 2)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "原发病诊断");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "310");
                itemNode.SetAttribute("name", "原发病诊断分类");
                itemNode.SetAttribute("type", "13");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "慢性肾脏病或慢性肾衰竭";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10001");
                itemNode.SetAttribute("name", "慢性肾脏病分期-CKD");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "G1";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10002");
                itemNode.SetAttribute("name", "慢性肾脏病分期-CKD尿蛋白");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "A1";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "311");
                itemNode.SetAttribute("name", "慢性肾功能衰竭或慢性肾脏病");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "继发性肾小球疾病";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "314");
                itemNode.SetAttribute("name", "继发性肾小球疾病");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "继发性肾小球疾病";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "905");
                itemNode.SetAttribute("name", "原发病诊断描述");
                itemNode.SetAttribute("type", "3");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "原发病诊断，丙型肝炎病毒相关性肾炎";
            }

            //艾滋病
            //关联检验结果
            filterlisResult = lisResultList.Where(t => t.item.F_ItemCode.Equals("HIV")).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "58");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "艾滋病");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10517");
                itemNode.SetAttribute("name", "艾滋病");
                itemNode.SetAttribute("type", "13");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HIV")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }
            }

            //过敏诊断
            if (1 == 2)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "35");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "过敏诊断");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "367");
                itemNode.SetAttribute("name", "过敏反应");
                itemNode.SetAttribute("type", "13");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "透析器材过敏";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "369");
                itemNode.SetAttribute("name", "透析器材过敏");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "本次使用膜材料";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "370");
                itemNode.SetAttribute("name", "透析膜");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "醋酸纤维素膜";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "311");
                itemNode.SetAttribute("name", "慢性肾功能衰竭或慢性肾脏病");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "继发性肾小球疾病";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "314");
                itemNode.SetAttribute("name", "继发性肾小球疾病");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "继发性肾小球疾病";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "905");
                itemNode.SetAttribute("name", "原发病诊断描述");
                itemNode.SetAttribute("type", "3");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "原发病诊断，丙型肝炎病毒相关性肾炎";
            }

            //乙肝检查
            tempArr = new List<string>
                {
                    "HBVDNA",
                    "HBeAg",
                    "Anti-HBe",
                    "Anti-HBs",
                    "HBsAg",
                    "Anti-HBc"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "54");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "乙肝二对半");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "506");
                itemNode.SetAttribute("name", "HBsAg");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HBsAg")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "507");
                itemNode.SetAttribute("name", "AntiHBs");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Anti-HBs")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "508");
                itemNode.SetAttribute("name", "HBeAg");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HBeAg")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "509");
                itemNode.SetAttribute("name", "AntiHBe");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Anti-HBe")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "510");
                itemNode.SetAttribute("name", "AntiHBc");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Anti-HBc")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

            }

            //CRP与B2-MG及血糖
            tempArr = new List<string>
                {
                    "CRP",
                    "β2-MG",
                    "GLU",
                    "HbA1c"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "53");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "CRP与B2-MG及血糖");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "504");
                itemNode.SetAttribute("name", "C反应蛋白(mg/L)");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("CRP")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "505");
                itemNode.SetAttribute("name", "β2微球蛋白(mg/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("β2-MG")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "499");
                itemNode.SetAttribute("name", "葡萄糖(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("GLU")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "4992");
                itemNode.SetAttribute("name", "糖化血红蛋白(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HbA1c")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "53_progress");
                itemNode.SetAttribute("name", "透前透后");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "透前";

            }

            //结核
            tempArr = new List<string>
                {
                    "结核抗体",
                    "结核菌素试验"
                    //"T-SPOT"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "60");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "结核");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "522");
                itemNode.SetAttribute("name", "结核抗体");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("结核抗体")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "60_01");
                itemNode.SetAttribute("name", "结核菌素试验");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("结核菌素试验")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10523");
                itemNode.SetAttribute("name", "T-SPOT");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("T-SPOT")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";
                }
            }

            //颈动脉超声
            if (1 == 2)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "64");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "颈动脉超声");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "551");
                itemNode.SetAttribute("name", "内膜增厚");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "阴性";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "552");
                itemNode.SetAttribute("name", "中膜增厚");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "阴性";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "553");
                itemNode.SetAttribute("name", "斑块形成");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "阴性";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "554");
                itemNode.SetAttribute("name", "狭窄");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "阴性";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "64_01");
                itemNode.SetAttribute("name", "闭塞");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "阴性";
            }

            //心电图检查
            if (1 == 1)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "62");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "心电图检查");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "527");
                itemNode.SetAttribute("name", "心电图检查诊断");
                itemNode.SetAttribute("type", "13");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                itemNode.InnerText = "正常";
            }

            //生化检查
            tempArr = new List<string>
                {
                    "ALT",
                    "AST",
                    "ALP",/////
                    "γ-GT",/////
                    "LDH",/////
                    "CK",//
                    "TCHO",
                    "LDL",
                    "HDL",
                    "TG",
                    "ALB-A",
                    "TP",
                    "ALB-A",
                    "GLU",
                    "UREA",
                    "CREA",
                    "UA",
                    "TCA",
                    "P",
                    "K",
                    "Na",
                    "Cl",
                    "CO2"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "LIS_713_1");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "生化检查");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "493");
                itemNode.SetAttribute("name", "丙氨酸氨基转移酶(U/L)");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("ALT")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "492");
                itemNode.SetAttribute("name", "天门冬氨酸氨基转移酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("AST")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_3");
                itemNode.SetAttribute("name", "碱性磷酸酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("ALP")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_4");
                itemNode.SetAttribute("name", "L-y-谷氨酰转肽酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("γ-GT")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_5");
                itemNode.SetAttribute("name", "乳酸脱氢酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("LDH")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_6");
                itemNode.SetAttribute("name", "肌酸激酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("CK")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "496");
                itemNode.SetAttribute("name", "总胆固醇(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("TCHO")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "495");
                itemNode.SetAttribute("name", "甘油三酯(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("TG")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "498");
                itemNode.SetAttribute("name", "高密度脂蛋白胆固醇(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HDL")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "497");
                itemNode.SetAttribute("name", "低密度脂蛋白胆固醇(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("LDL")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "490");
                itemNode.SetAttribute("name", "总蛋白质(g/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("TP")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "491");
                itemNode.SetAttribute("name", "白蛋白(g/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("ALB-A")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_18");
                itemNode.SetAttribute("name", "球蛋白(g/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("ALB-A")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "499");
                itemNode.SetAttribute("name", "葡萄糖(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("GLU")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "487");
                itemNode.SetAttribute("name", "尿素(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("UREA")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "489");
                itemNode.SetAttribute("name", "肌酐(μmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("CREA")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10489");
                itemNode.SetAttribute("name", "尿酸(μmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("UA")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "479");
                itemNode.SetAttribute("name", "钙(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("TCA")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "480");
                itemNode.SetAttribute("name", "磷(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("P")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "479480");
                itemNode.SetAttribute("name", "钙磷乘积（mg²/dl²）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("P*C")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_Mg_001");
                itemNode.SetAttribute("name", "镁(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("P")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "500");
                itemNode.SetAttribute("name", "钾(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("K")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "501");
                itemNode.SetAttribute("name", "钠(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Na")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "502");
                itemNode.SetAttribute("name", "氯(mmol/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Cl")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }


                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_30");
                itemNode.SetAttribute("name", "视黄醇结合蛋白(mg/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Cl")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_31");
                itemNode.SetAttribute("name", "腺苷脱氨酶(U/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Cl")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_32");
                itemNode.SetAttribute("name", "血沉(mm/h)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Cl")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null)
                //{
                //    tempNode.InnerText = findrow.F_Result;
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "503");
                itemNode.SetAttribute("name", "二氧化碳结合力（CO2CP）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("CO2")).Select(t => t.item).FirstOrDefault();
                if (findrow != null)
                {
                    tempNode.InnerText = findrow.F_Result;
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "LIS_713_1_progress");
                itemNode.SetAttribute("name", "透前透后");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "透前";

            }

            //透析器,滤器（质控）
            if (1 == 1)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30040");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "透析器,滤器（质控）");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30427");
                itemNode.SetAttribute("name", "类型");
                itemNode.SetAttribute("type", "13");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "国产";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30428");
                itemNode.SetAttribute("name", "通量");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "高通量";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30429");
                itemNode.SetAttribute("name", "使用");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "一次性使用";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30430");
                itemNode.SetAttribute("name", "透析膜");
                itemNode.SetAttribute("type", "13");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "聚砜（PS）膜,聚酰胺膜,";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "433");
                itemNode.SetAttribute("name", "膜面积");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "1.2≤膜面积&lt;1.4(㎡)";

            }

            //既往史
            //查询病历信息
            var medicalRecord = _uow.GetRepository<MedicalRecordEntity>().IQueryable()
                .Where(t => t.F_Pid==patientId && t.F_ContentTime >= startDate && t.F_ContentTime <= endDate && t.F_AuditFlag == true && t.F_EnabledMark != false)
                .OrderByDescending(t => t.F_ContentTime)
                .Select(t => new
                {
                    t.F_ContentTime,
                    t.F_Content
                })
                .FirstOrDefault();
            if (medicalRecord != null)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "80000");
                sectionsNode.SetAttribute("actualExamineDate", medicalRecord.F_ContentTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "既往史");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "800001");
                itemNode.SetAttribute("name", "既往史详情");
                itemNode.SetAttribute("type", "3");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = medicalRecord.F_Content + "";
            }

            //HCV-RNA
            tempArr = new List<string>
                {
                    "HCVRNA"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "57");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "HCV-RNA");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "515");
                itemNode.SetAttribute("name", "HCV-RNA");
                itemNode.SetAttribute("type", "2");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HCVRNA")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    tempNode.InnerText = findrow.F_Result + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "516");
                itemNode.SetAttribute("name", "HCV-RNA 2");
                itemNode.SetAttribute("type", "2");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);

            }

            //梅毒
            tempArr = new List<string>
                {
                    "梅毒"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "59");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "梅毒");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "520");
                itemNode.SetAttribute("name", "梅毒");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("梅毒")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";

                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "5200");
                itemNode.SetAttribute("name", "TRUST");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "5201");
                itemNode.SetAttribute("name", "TRUST滴度检测");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "5202");
                itemNode.SetAttribute("name", "TPPA");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
            }

            //透析处方
            var visitCountList = _uow.GetRepository<PatVisitEntity>().IQueryable().
                Where(t => t.F_Pid==patientId && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_DialysisEndTime != null && t.F_DialysisStartTime != null && t.F_EnabledMark != false && t.F_DeleteMark != true)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_DialysisType,
                    t.F_DialysisStartTime,
                    t.F_DialysisEndTime
                })
                .ToList();
            if (visitCountList.Count > 0)
            {
                var totalDays = (endDate2 - startDate).TotalDays.ToInt();
                if (DateTime.Today < endDate)
                {
                    totalDays = (DateTime.Today - startDate).TotalDays.ToInt();
                }
                if (totalDays == 0) totalDays = 1;
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30164");
                sectionsNode.SetAttribute("actualExamineDate", DateTime.Now.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "血透处方");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30935");
                itemNode.SetAttribute("name", "HD次数（次/每周）");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrows = visitCountList
                    .Where(t => t.F_DialysisType == "HD" || t.F_DialysisType == "HFHD")
                    .ToList();
                if (findrows.Count > 0)
                {
                    var intTemp = (findrows.Count * 7f / totalDays).ToFloat(0); //四舍五入
                    tempNode.InnerText = (intTemp == 0 ? 1 : intTemp) + "";
                }
                else
                {
                    tempNode.InnerText = "0";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "301394");
                itemNode.SetAttribute("name", "HD次数（次/每周）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (findrows.Count > 0)
                {
                    var hours = findrows.Average(t => (t.F_DialysisEndTime.ToDate() - t.F_DialysisStartTime.ToDate()).TotalHours).ToInt();
                    tempNode.InnerText = hours + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "3011403");
                itemNode.SetAttribute("name", "HDF（次/每周）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrows = visitCountList
                    .Where(t => t.F_DialysisType.Equals("HDF"))
                    .ToList();
                if (findrows.Count > 0)
                {
                    var intTemp = (findrows.Count * 7f / totalDays).ToFloat(0); //四舍五入
                    tempNode.InnerText = (intTemp == 0 ? 1 : intTemp) + "";
                }
                else
                {
                    tempNode.InnerText = "0";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30936");
                itemNode.SetAttribute("name", "HDF治疗时间（小时/次）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (findrows.Count > 0)
                {
                    var hours = findrows.Average(t => (t.F_DialysisEndTime.ToDate() - t.F_DialysisStartTime.ToDate()).TotalHours).ToInt();
                    tempNode.InnerText = hours + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30937");
                itemNode.SetAttribute("name", "HP（次/每周）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrows = visitCountList
                    .Where(t => t.F_DialysisType.Equals("HD+HP"))
                    .ToList();
                if (findrows.Count > 0)
                {
                    var intTemp = (findrows.Count * 7f / totalDays).ToFloat(0); //四舍五入
                    tempNode.InnerText = (intTemp == 0 ? 1 : intTemp) + "";
                }
                else
                {
                    tempNode.InnerText = "0";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "309371");
                itemNode.SetAttribute("name", "HP治疗时间（小时/次）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (findrows.Count > 0)
                {
                    var hours = findrows.Average(t => (t.F_DialysisEndTime.ToDate() - t.F_DialysisStartTime.ToDate()).TotalHours).ToInt();
                    tempNode.InnerText = hours + "";
                }

            }

            //血压、心率测量（质控）
            var visitRows = _uow.GetRepository<PatVisitEntity>().IQueryable().
                Where(t => t.F_Pid==patientId && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_DialysisEndTime != null && t.F_DialysisStartTime != null && t.F_EnabledMark != false && t.F_DeleteMark != true)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_SystolicPressure,
                    t.F_DiastolicPressure,
                    t.F_Pulse,
                    t.F_Temperature
                })
                .ToList();
            var obRows = _uow.GetRepository<DialysisObservationEntity>().IQueryable().
                Where(t => t.F_Pid == patient.F_Id && t.F_VisitDate >= startDate && t.F_VisitDate <= endDate && t.F_EnabledMark != false && t.F_DeleteMark != true)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_SSY,
                    t.F_SZY,
                    t.F_HR,
                    t.F_NurseOperatorTime
                })
                .ToList();

            //筛选数据比较完整的记录
            var joinData = visitRows.GroupJoin(obRows, a => a.F_VisitDate, b => b.F_VisitDate, (a, b) => new
            {
                a.F_VisitDate,
                a.F_SystolicPressure,
                a.F_DiastolicPressure,
                a.F_Pulse,
                a.F_Temperature,
                count = b.Count(),
                rows = b.Select(r => new
                {
                    r.F_SSY,
                    r.F_SZY,
                    r.F_HR,
                    r.F_NurseOperatorTime
                }).OrderBy(r => r.F_NurseOperatorTime)
            }).Where(t => t.count > 0).OrderByDescending(t => t.count).ThenBy(t => t.F_VisitDate).FirstOrDefault();

            if (joinData != null)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30041");
                sectionsNode.SetAttribute("actualExamineDate", joinData.F_VisitDate.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "血压、心率测量（质控）");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30434");
                itemNode.SetAttribute("name", "测量部位");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "上肢";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30041_01");
                itemNode.SetAttribute("name", "透析相关高血压");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "无";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30041_02");
                itemNode.SetAttribute("name", "透析相关低血压");
                itemNode.SetAttribute("type", "11");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "无";

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30435");
                itemNode.SetAttribute("name", "透析前收缩压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.F_SystolicPressure.HasValue)
                {
                    tempNode.InnerText = joinData.F_SystolicPressure.ToFloat(0) + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30436");
                itemNode.SetAttribute("name", "透析前舒张压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.F_DiastolicPressure.HasValue)
                {
                    tempNode.InnerText = joinData.F_DiastolicPressure.ToFloat(0) + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30435_average_3m");
                itemNode.SetAttribute("name", "透前平均收缩压(mmHg)");
                itemNode.SetAttribute("type", "34");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //if (joinData.F_DiastolicPressure.HasValue)
                //{
                //    tempNode.InnerText = joinData.F_DiastolicPressure.ToFloat(0) + "";
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30436_average_3m");
                itemNode.SetAttribute("name", "透前平均舒张压(mmHg)");
                itemNode.SetAttribute("type", "34");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //if (joinData.F_DiastolicPressure.HasValue)
                //{
                //    tempNode.InnerText = joinData.F_DiastolicPressure.ToFloat(0) + "";
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30437");
                itemNode.SetAttribute("name", "透析后收缩压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.rows.Last().F_SSY.HasValue)
                {
                    tempNode.InnerText = joinData.rows.Last().F_SSY.ToFloat(0) + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30438");
                itemNode.SetAttribute("name", "透析后舒张压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.rows.Last().F_SZY.HasValue)
                {
                    tempNode.InnerText = joinData.rows.Last().F_SZY.ToFloat(0) + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30439");
                itemNode.SetAttribute("name", "非透析日收缩压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //if (joinData.rows.Last().F_SZY.HasValue)
                //{
                //    tempNode.InnerText = joinData.rows.Last().F_SZY.ToFloat(0) + "";
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30440");
                itemNode.SetAttribute("name", "非透析日舒张压(mmHg)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                //if (joinData.rows.Last().F_SZY.HasValue)
                //{
                //    tempNode.InnerText = joinData.rows.Last().F_SZY.ToFloat(0) + "";
                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30441");
                itemNode.SetAttribute("name", "透析前心率（次/分）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.F_Pulse.HasValue)
                {
                    tempNode.InnerText = joinData.F_Pulse.ToFloat(0) + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30442");
                itemNode.SetAttribute("name", "透析后心率（次/分）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (joinData.rows.Last().F_HR.HasValue)
                {
                    tempNode.InnerText = joinData.rows.Last().F_HR.ToFloat(0) + "";
                }
            }

            //HBVDNA
            tempArr = new List<string>
                {
                    "HBVDNA"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "55");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "HBVDNA");

                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HBVDNA")).Select(t => t.item).FirstOrDefault();

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10511");
                itemNode.SetAttribute("name", "HBVDNA");
                itemNode.SetAttribute("type", "2");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = findrow.F_Result + findrow.F_Unit + "";
                //var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("梅毒")).Select(t => t.item).FirstOrDefault();
                //if (findrow != null && !findrow.F_Result.IsEmpty())
                //{
                //    var resultTemp = findrow.F_Result + "";
                //    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";

                //}

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "10513");
                itemNode.SetAttribute("name", "HBVDNA 2");
                itemNode.SetAttribute("type", "2");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "105110");
                itemNode.SetAttribute("name", "HBVDNA");
                itemNode.SetAttribute("type", "1");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);


                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float copys))
                    {
                        if (copys < 1000)
                        {
                            tempNode.InnerText = "阴性";
                        }
                        else
                        {
                            tempNode.InnerText = "阳性";
                        }
                    }
                    //tempNode.InnerText = findrow.F_Result + "";
                }
                //if (filterlisResult.f)
                //itemNode.InnerText = (patient.F_HBsAg + "").Contains("+") || (patient.F_HBsAg + "").Contains("阳") ? "阳性" : "阴性";

            }

            //血管通路
            var vascularAccess = _uow.GetRepository<VascularAccessEntity>()
                .IQueryable()
                .FirstOrDefault(t => t.F_Pid==patientId && t.F_OperateTime > startDate && t.F_OperateTime < endDate2 && t.F_EnabledMark == true);
            if (vascularAccess != null)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30166");
                sectionsNode.SetAttribute("actualExamineDate", vascularAccess.F_OperateTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "血管通路");

                if (!vascularAccess.F_VascularAccess.IsEmpty())
                {
                    if (vascularAccess.F_VascularAccess.Equals("自体内瘘"))
                    {
                        var itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30940");
                        itemNode.SetAttribute("name", "血管通路类型");
                        itemNode.SetAttribute("type", "11");
                        var tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        itemNode.InnerText = "自体内瘘";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30166_position");
                        itemNode.SetAttribute("name", "血管通路位置(左-右)");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        itemNode.InnerText = vascularAccess.F_BodyPosition ?? "左";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30944");
                        itemNode.SetAttribute("name", "自体内瘘位置");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        itemNode.InnerText = vascularAccess.F_BodyPart.Equals("腕部") ? "前臂" :
                            vascularAccess.F_BodyPart.Equals("肘部") ? "上臂" :
                            vascularAccess.F_BodyPart.Equals("下肢") ? "下肢" : "前臂";

                    }
                    else if (vascularAccess.F_VascularAccess.Equals("移植内瘘"))
                    {
                        var itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30940");
                        itemNode.SetAttribute("name", "血管通路类型");
                        itemNode.SetAttribute("type", "11");
                        var tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = "移植血管";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30166_position");
                        itemNode.SetAttribute("name", "血管通路位置(左-右)");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPosition ?? "左";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "309451");
                        itemNode.SetAttribute("name", "移植血管位置");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPart.Equals("腕部") ? "前臂" :
                            vascularAccess.F_BodyPart.Equals("肘部") ? "上臂" :
                            vascularAccess.F_BodyPart.Equals("下肢") ? "下肢" : "前臂";

                    }
                    else if (vascularAccess.F_VascularAccess.Equals("直接动静脉穿刺"))
                    {
                        var itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30940");
                        itemNode.SetAttribute("name", "血管通路类型");
                        itemNode.SetAttribute("type", "11");
                        var tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        itemNode.InnerText = "直接动静脉穿刺";
                    }
                    else if (vascularAccess.F_Type.Equals("暂时性"))
                    {
                        var itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30940");
                        itemNode.SetAttribute("name", "血管通路类型");
                        itemNode.SetAttribute("type", "11");
                        var tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = "临时中心静脉置管";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30166_position");
                        itemNode.SetAttribute("name", "血管通路位置(左-右)");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPosition ?? "左";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30942");
                        itemNode.SetAttribute("name", "临时中心静脉置管位置");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPart.Equals("颈内静脉") ? "颈内静脉" :
                            vascularAccess.F_BodyPart.Equals("锁骨下静脉") ? "锁骨下静脉" : "颈内静脉";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "3094200");
                        itemNode.SetAttribute("name", "导管种类");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_VascularAccess.Equals("带隧道带涤纶套导管") ? "带隧道带涤纶套股静脉导管" :
                            vascularAccess.F_VascularAccess.Equals("无隧道无涤纶套导管") ? "无隧道无涤纶套股静脉导管" : "带隧道带涤纶套股静脉导管";
                    }
                    else if (vascularAccess.F_Type.Equals("永久性") || vascularAccess.F_Type.Equals("半永久性"))
                    {
                        var itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30940");
                        itemNode.SetAttribute("name", "血管通路类型");
                        itemNode.SetAttribute("type", "11");
                        var tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = "临时中心静脉置管";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30166_position");
                        itemNode.SetAttribute("name", "血管通路位置(左-右)");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPosition ?? "左";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "30943");
                        itemNode.SetAttribute("name", "长期中心静脉置管位置");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_BodyPart.Equals("颈内静脉") ? "颈内静脉" :
                            vascularAccess.F_BodyPart.Equals("锁骨下静脉") ? "锁骨下静脉" : "颈内静脉";

                        itemNode = xdoc.CreateElement("item");
                        sectionsNode.AppendChild(itemNode);
                        itemNode.SetAttribute("id", "3094200");
                        itemNode.SetAttribute("name", "导管种类");
                        itemNode.SetAttribute("type", "11");
                        tempNode = xdoc.CreateElement("value");
                        itemNode.AppendChild(tempNode);
                        tempNode.InnerText = vascularAccess.F_VascularAccess.Equals("带隧道带涤纶套导管") ? "带隧道带涤纶套股静脉导管" :
                            vascularAccess.F_VascularAccess.Equals("无隧道无涤纶套导管") ? "无隧道无涤纶套股静脉导管" : "带隧道带涤纶套股静脉导管";
                    }
                }

            }

            //合并其他透析模式（血透）
            if (1 == 1)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30045");
                sectionsNode.SetAttribute("actualExamineDate", endDate.ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "合并其他透析模式（血透）");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30045_01");
                itemNode.SetAttribute("name", "合并其他透析模式");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                tempNode.InnerText = "无";
            }

            //慢性并发症诊断
            //透析液（质控）
            //传染病诊断
            //病理诊断
            //急性并发症诊断
            //超声心动图检查

            //HCV
            tempArr = new List<string>
                {
                    "Anti-HCV"
                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "56");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "HCV");

                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("Anti-HCV")).Select(t => t.item).FirstOrDefault();

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "514");
                itemNode.SetAttribute("name", "AntiHCV");
                itemNode.SetAttribute("type", "11");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    var resultTemp = findrow.F_Result + "";
                    tempNode.InnerText = resultTemp.IndexOf("阳") >= 0 || resultTemp.IndexOf("+") > 0 ? "阳性" : "阴性";

                }
            }

            //透析充分性
            var cfxRecords = _uow.GetRepository<ProcessFlowEntity>().IQueryable()
                .Where(t => t.F_Pid.Equals(patientId) && t.F_VisitDate >= startDate
                && t.F_VisitDate <= endDate && t.F_EnabledMark != false && t.F_DeleteMark != true
                && t.F_PreUrea != null && t.F_PreWeight != null && t.F_PostUrea != null && t.F_PostWeight != null)
                .Select(t => new
                {
                    t.F_VisitDate,
                    t.F_TotalHours,
                    t.F_PostUrea,
                    t.F_PostWeight,
                    t.F_PreUrea,
                    t.F_PreWeight,
                    t.F_Result
                })
                .FirstOrDefault();
            if (cfxRecords != null)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "30042");
                sectionsNode.SetAttribute("actualExamineDate", cfxRecords.F_VisitDate.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "透析充分性");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "304410");
                itemNode.SetAttribute("name", "身高（cm）");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (patient.F_Height.HasValue)
                {
                    itemNode.InnerText = patient.F_Height.ToInt() + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "304420");
                itemNode.SetAttribute("name", "透前体重（kg）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (cfxRecords.F_PreWeight.HasValue)
                {
                    itemNode.InnerText = cfxRecords.F_PreWeight.ToInt() + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30443");
                itemNode.SetAttribute("name", "透后体重（kg）");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (cfxRecords.F_PostWeight.HasValue)
                {
                    itemNode.InnerText = cfxRecords.F_PostWeight.ToInt() + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30446");
                itemNode.SetAttribute("name", "透前尿素");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (cfxRecords.F_PreUrea.HasValue)
                {
                    itemNode.InnerText = cfxRecords.F_PreUrea.ToInt() + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30447");
                itemNode.SetAttribute("name", "透后尿素");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (cfxRecords.F_PostUrea.HasValue)
                {
                    itemNode.InnerText = cfxRecords.F_PostUrea.ToInt() + "";
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "30451");
                itemNode.SetAttribute("name", "spKt/V");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                if (cfxRecords.F_Result.HasValue)
                {
                    itemNode.InnerText = cfxRecords.F_Result.ToInt() + "";
                }

            }

            //抗凝剂（质控）

            //骨矿物质代谢

            //肿瘤诊断

            //胸部X线检查

            //血常规
            tempArr = new List<string>
                {
                    "HGB",
                    "PLT",
                    "WBC",
                    "HCT"

                };
            filterlisResult = lisResultList.Where(t => tempArr.Contains(t.item.F_ItemCode)).ToList();
            if (filterlisResult.Count > 0)
            {
                var sectionsNode = xdoc.CreateElement("sections");
                commonItemNode.AppendChild(sectionsNode);
                sectionsNode.SetAttribute("sectionID", "49");
                sectionsNode.SetAttribute("actualExamineDate", filterlisResult.OrderByDescending(t => t.item.F_ReportTime).FirstOrDefault().item.F_ReportTime.ToDate().ToDateTimeString().Substring(0, 10));
                sectionsNode.SetAttribute("sectionName", "血常规");

                var itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "475");
                itemNode.SetAttribute("name", "白细胞（10^9/L）");
                itemNode.SetAttribute("type", "5");
                var tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                var findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("WBC")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float tempf))
                    {
                        tempNode.InnerText = tempf + "";
                    }
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "476");
                itemNode.SetAttribute("name", "血红蛋白(g/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HGB")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float tempf))
                    {
                        tempNode.InnerText = tempf + "";
                    }
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "476");
                itemNode.SetAttribute("name", "血红蛋白(g/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HGB")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float tempf))
                    {
                        tempNode.InnerText = tempf + "";
                    }
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "477");
                itemNode.SetAttribute("name", "红细胞压积(%)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("HCT")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float tempf))
                    {
                        tempNode.InnerText = tempf + "";
                    }
                }

                itemNode = xdoc.CreateElement("item");
                sectionsNode.AppendChild(itemNode);
                itemNode.SetAttribute("id", "478");
                itemNode.SetAttribute("name", "血小板(10^9/L)");
                itemNode.SetAttribute("type", "5");
                tempNode = xdoc.CreateElement("value");
                itemNode.AppendChild(tempNode);
                findrow = filterlisResult.Where(t => t.item.F_ItemCode.Equals("PLT")).Select(t => t.item).FirstOrDefault();
                if (findrow != null && !findrow.F_Result.IsEmpty())
                {
                    if (float.TryParse(findrow.F_Result, out float tempf))
                    {
                        tempNode.InnerText = tempf + "";
                    }
                }
            }
            return xdoc;
        }

        public XmlDocument CombineXmlFile(string ids, string rootPath)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration xdec = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(xdec);
            XmlElement xroot = xdoc.CreateElement("patientList");
            xdoc.AppendChild(xroot);
            //var arr = new List<string>();
            //foreach(var item in ids.ToJArrayObject())
            //{
            //    arr.Add(item.Value<string>("id"));
            //}
            var arr = ids.Split(',');
            var list = _service.IQueryable(t => arr.Contains(t.F_Id)).ToList();
            foreach (var item in list)
            {
                var fullpath = Path.Combine(rootPath, item.F_MonthDesc, item.F_FileName);
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(fullpath);
                    var sourceNode = document.SelectSingleNode("patientList/patient");
                    XmlElement targetNode = xdoc.CreateElement("patient");
                    xroot.AppendChild(targetNode);
                    targetNode.SetAttribute("id", item.F_PId);
                    targetNode.InnerXml = sourceNode.InnerXml;

                    item.F_HasDownload = true;
                    item.F_DownloadTime = DateTime.Now;
                    _service.Update(item, true);
                }
                catch
                {
                    continue;
                }
            }
            return xdoc;
        }

        public void SubmitData(string patientId, string month, string fileName, string filePath, string userId)
        {
            var expression = ExtLinq.True<DataReportJSEntity>();
            expression = expression.And(t => t.F_PId == patientId);
            expression = expression.And(t => t.F_MonthDesc == month);
            var entity = _service.IQueryable(expression).FirstOrDefault();
            if (entity == null)
            {
                entity = new DataReportJSEntity
                {
                    F_PId = patientId,
                    F_MonthDesc = month,
                    F_FileName = fileName,
                    F_FilePath = filePath,
                    F_IsCompleted = true,
                    F_HasDownload = false,
                    F_Id = Common.GuId(),
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = userId
                };
                _service.Insert(entity);
            }
            else
            {
                entity.F_FileName = fileName;
                entity.F_FilePath = filePath;
                entity.F_IsCompleted = true;
                entity.F_DownloadTime = null;
                entity.F_HasDownload = false;
                entity.F_CreatorTime = DateTime.Now;
                entity.F_CreatorUserId = userId;
                _service.Update(entity, true);
            }
        }
    }
}
