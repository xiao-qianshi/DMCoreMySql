using Dmt.DM.Application.SystemManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IMedicalRecordApp : IScopedAppService
    {
        Task<List<MedicalRecordEntity>> GetList(Pagination pagination, string keyword);
        Task<List<MedicalRecordEntity>> GetListByPid(Pagination pagination, string pid);
        IQueryable<MedicalRecordEntity> GetList();
        Task<MedicalRecordEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        IQueryable<MedicalRecordEntity> GetList(string patientId, DateTime? startDate, DateTime? endDate);
        Task<int> UpdateForm(MedicalRecordEntity entity);
        Task<int> SubmitForm<TDto>(MedicalRecordEntity entity, TDto dto) where TDto : class;
        Task<int> InsertForm(MedicalRecordEntity entity);
        Task<string> GetReport(string keyValue);
    }

    public class MedicalRecordApp : IMedicalRecordApp
    {
        private readonly IRepository<MedicalRecordEntity> _service = null;
        private IUnitOfWork _uow = null;
        //private IHttpContextAccessor _httpContext = null;
        private readonly IOrganizeApp _organizeApp;
        private readonly IUsersService _usersService;
        private readonly IPatientApp _patientApp;

        public MedicalRecordApp(IUnitOfWork uow, IUsersService usersService, IOrganizeApp organizeApp, IPatientApp patientApp)
        {
            _uow = uow;
            _service = _uow.GetRepository<MedicalRecordEntity>();
            _organizeApp = organizeApp;
            _usersService = usersService;
            _patientApp = patientApp;
        }

        public Task<List<MedicalRecordEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<MedicalRecordEntity>();
            string pid = string.Empty;
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(keyword))
            {
                string[] arr = keyword.Split('^');
                pid = arr[0];
                condition = arr[1];
            }
            if (!string.IsNullOrEmpty(pid))
            {
                expression = expression.And(t => t.F_Pid == pid);
                //expression = expression.And(t => t.F_OrderText.Contains(condition));
            }
            else
            {
                expression = expression.And(t => t.F_Id == "");
            }

            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<MedicalRecordEntity>> GetListByPid(Pagination pagination, string pid)
        {
            var expression = ExtLinq.True<MedicalRecordEntity>();
            expression = expression.And(t => t.F_Pid == pid);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.FindListAsync(expression, pagination);
        }

        public IQueryable<MedicalRecordEntity> GetList()
        {
            return _service.IQueryable();
        }
        public Task<MedicalRecordEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            return _service.DeleteAsync(t => t.F_Id == keyValue);
        }

        public IQueryable<MedicalRecordEntity> GetList(string patientId, DateTime? startDate, DateTime? endDate)
        {
            var expression = ExtLinq.True<MedicalRecordEntity>();
            expression = expression.And(t => t.F_Pid == patientId);
            if (startDate.HasValue && endDate.HasValue)
            {
                var date1 = startDate.ToDate().Date;
                var date2 = endDate.ToDate().Date.AddDays(1);
                expression = expression.And(t => t.F_ContentTime >= date1 && t.F_ContentTime <= date2);
            }
            else
            {
                var date1 = DateTime.Today.AddDays(-30);
                var date2 = DateTime.Today.AddDays(1);
                expression = expression.And(t => t.F_ContentTime >= date1 && t.F_ContentTime <= date2);
            }
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).OrderByDescending(t => t.F_ContentTime);
        }

        public Task<int> UpdateForm(MedicalRecordEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(MedicalRecordEntity entity, TDto dto) where TDto : class
        {
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                return _service.InsertAsync(entity);
            }
        }

        public Task<int> InsertForm(MedicalRecordEntity entity)
        {
            return _service.InsertAsync(entity);
        }

        public async Task<string> GetReport(string keyValue)
        {
            var result = new StringBuilder();
            result.Append("");
            //查询医院名称
            var hospitalName = await _organizeApp.GetHospitalName();
            //查询患者基本信息
            var patient = await _patientApp.GetForm(keyValue);// patientApp.GetList(keyValue).FirstOrDefault();
            //查询所有病程记录
            var list = from m in GetList()
                       where m.F_AuditFlag == true && m.F_Pid == keyValue
                       select new
                       {
                           m.F_Title,
                           m.F_ContentTime,
                           m.F_Content,
                           m.F_CreatorUserId,
                           m.F_AuditTime
                       };
            //字符    十进制 转义字符
            //" &#34;   &quot;
            //& &#38;	&amp;
            //< &#60;	&lt;
            //> &#62;	&gt;
            //不断开空格(non - breaking space) &#160;	&nbsp;
            //构建页眉
            result.Append("&lt;div class=\"price-title\"&gt;" + "病&nbsp;&nbsp;&nbsp;&nbsp;程&nbsp;&nbsp;&nbsp;&nbsp;记&nbsp;&nbsp;&nbsp;&nbsp;录" + "&lt;/div&gt;");
            result.Append("&lt;div class=\"price-subtitle\"&gt;" + hospitalName + "&lt;/div&gt;");
            result.Append("&lt;div class=\"price-info\"&gt;");
            result.Append("&lt;table class=\"form\"&gt;");
            result.Append("&lt;tr&gt;");
            result.Append("&lt;th class=\"formTitle\"&gt;" + "姓名" + "&lt;/th&gt;");
            result.Append("&lt;td class=\"formValue\"  style=\"padding-right: 20px;\"&gt;");
            result.Append("&lt;input type=\"text\" class=\"form-control\" value=\"" + patient.F_Name + "\"/&gt;");
            result.Append("&lt;/td&gt;");
            //result.Append("&lt;/tr&gt;");

            //result.Append("&lt;tr&gt;");
            result.Append("&lt;th class=\"formTitle\"&gt;" + "性别" + "&lt;/th&gt;");
            result.Append("&lt;td class=\"formValue\"  style=\"padding-right: 20px;\"&gt;");
            result.Append("&lt;input type=\"text\" class=\"form-control\" value=\"" + patient.F_Gender + "\"/&gt;");
            result.Append("&lt;/td&gt;");
            //result.Append("&lt;/tr&gt;");

            //result.Append("&lt;tr&gt;");
            result.Append("&lt;th class=\"formTitle\"&gt;" + "年龄" + "&lt;/th&gt;");
            result.Append("&lt;td class=\"formValue\"  style=\"padding-right: 20px;\"&gt;");
            result.Append("&lt;input type=\"text\" class=\"form-control\" value=\"" +
                (patient.F_BirthDay == null ? "&nbsp;&nbsp;&nbsp;" : (DateTime.Now.Subtract((DateTime)patient.F_BirthDay).Days / 365).ToString())
                + "岁\"/&gt;");
            result.Append("&lt;/td&gt;");

            result.Append("&lt;th class=\"formTitle\"&gt;" + "透析号" + "&lt;/th&gt;");
            result.Append("&lt;td class=\"formValue\"  style=\"padding-right: 20px;\"&gt;");
            result.Append("&lt;input type=\"text\" class=\"form-control\" value=\"" + patient.F_DialysisNo + "\"/&gt;");
            result.Append("&lt;/td&gt;");

            result.Append("&lt;/tr&gt;");
            result.Append("&lt;/table&gt;");
            result.Append("&lt;hr style=\"height:1px;border:none;border-top:1px solid #555555;\" /&gt;");
            result.Append("&lt;/div&gt;");

            //病历内容
            foreach (var item in list.ToList().OrderBy(t => t.F_ContentTime))
            {
                result.Append("&lt;div class=\"price-table-content\"&gt;");
                result.Append("&lt;table&gt;");
                result.Append("&lt;tbody&gt;");
                //第一行 时间  标题
                result.Append("&lt;tr&gt;");
                result.Append("&lt;td&gt;");
                result.Append(item.F_ContentTime.ToDateTimeString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + item.F_Title);
                result.Append("&lt;/td&gt;");
                //result.Append("&lt;td&gt;");
                //result.Append(item.F_Title);
                //result.Append("&lt;/td&gt;");
                result.Append("&lt;/tr&gt;");
                //第二行 正文
                result.Append("&lt;tr&gt;");
                result.Append("&lt;td&gt;");
                result.Append("&lt;div&gt;");
                result.Append(item.F_Content);
                result.Append("&lt;/div&gt;");
                result.Append("&lt;/td&gt;");
                result.Append("&lt;/tr&gt;");
                //第三行 签名 时间
                result.Append("&lt;tr&gt;");
                result.Append("&lt;td&gt;");
                result.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;医师签名：");
                var user = await _usersService.FindUserAsync(item.F_CreatorUserId);// userApp.GetForm(item.F_CreatorUserId);
                if (user == null || string.IsNullOrWhiteSpace(user.F_RealName))
                {
                    result.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                else
                {
                    result.Append(user.F_RealName + "");
                }
                result.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;签名时间：" + item.F_AuditTime.ToDateTimeString());
                result.Append("&lt;/td&gt;");
                result.Append("&lt;/tr&gt;");

                result.Append("&lt;/tbody&gt;");
                result.Append("&lt;/table&gt;");
                result.Append("&lt;/div&gt;");
            }
            result.Append("&lt;br&gt;");
            result.Append("&lt;br&gt;");
            return result.ToString();
        }

    }
}
