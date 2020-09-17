using Dmt.Dm.Domain.Dto.QualityResult;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class QualityResultController : BaseApiController
    {
        private readonly IQualityResultApp _qualityResultApp;

        public QualityResultController(IQualityResultApp qualityResultApp)
        {
            _qualityResultApp = qualityResultApp;
        }

        public async Task<IActionResult> GetPagedGridJson(GetPagedGridInput input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_ReportTime",
                sord = input.orderType ?? "desc"
            };
            var keyword = input.patientId;
            var list = await _qualityResultApp.GetList(pagination, input.patientId, "", input.startDate, input.endDate);
            var data = new
            {
                rows = list.Select(t => new
                {
                    t.F_Id,
                    t.F_ReportTime,
                    t.F_ResultType,
                    t.F_ItemId,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_Result,
                    t.F_Unit,
                    t.F_LowerValue,
                    t.F_UpperValue,
                    //t.F_LowerCriticalValue,
                    //t.F_UpperCriticalValue,
                    F_ReferenceRange = t.F_LowerValue != null && t.F_UpperValue != null ? t.F_LowerValue.ToString() + " - " + t.F_UpperValue.ToString() : "",
                    t.F_Memo
                }),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }
    }
}
