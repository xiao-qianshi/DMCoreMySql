using Dmt.Dm.Domain.Dto.Nurse;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.PatientManage
{
    [Route("api/[controller]/[action]")]
    public class NurseController : BaseApiController
    {
        private readonly IDialysisObservationApp _dialysisObservationApp;
        private readonly IPatVisitApp _patVisitApp;
        private readonly IUsersService _usersService;

        public NurseController(IDialysisObservationApp dialysisObservationApp, IPatVisitApp patVisitApp, IUsersService usersService)
        {
            _dialysisObservationApp = dialysisObservationApp;
            _patVisitApp = patVisitApp;
            _usersService = usersService;
        }

        /// <summary>
        /// 新建观察记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SubmitData([FromBody]SubmitDataInput input)
        {
            var visit = await _patVisitApp.GetForm(input.id);
            if (visit == null)
            {
                return BadRequest("治疗单主键有误");
            }
            var user = await _usersService.GetCurrentUserAsync();
            var entity = new DialysisObservationEntity
            {
                F_Id = Common.GuId(),
                F_Pid = visit.F_Pid,
                F_VisitDate = visit.F_VisitDate,
                F_VisitNo = visit.F_VisitNo,
                F_SSY = input.ssy,
                F_SZY = input.szy,
                F_HR = input.hr,
                F_A = input.a,
                F_BF = input.bf,
                F_UFR = input.ufr,
                F_V = input.v,
                F_C = input.c,
                F_T = input.t,
                F_UFV = input.ufv,
                F_TMP = input.tmp,
                F_GSL = input.gsl,
                F_MEMO = input.memo,
                F_Nurse = user.F_RealName,
                F_NurseOperatorTime = input.operatorTime,
                F_EnabledMark = true,
                F_CreatorTime = DateTime.Now,
                F_CreatorUserId = user.F_Id,
            };
            await _dialysisObservationApp.InsertForm(entity);
            var data = new
            {
                id = entity.F_Id
            };
            return Ok(data);
        }

        /// <summary>
        /// 删除观察记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            if (input?.KeyValue == null)
            {
                return BadRequest("主键ID未传值！");
            }
            var entity = await _dialysisObservationApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return BadRequest("主键ID有误！");
            }
            var userId = _usersService.GetCurrentUserId();
            entity.F_EnabledMark = false;
            entity.F_DeleteMark = true;
            entity.F_DeleteTime = System.DateTime.Now;
            await _dialysisObservationApp.UpdateForm(entity);
            //var data = new
            //{
            //    message = "删除成功",
            //    id = entity.F_Id
            //};
            return Ok("删除成功");
        }
        /// <summary>
        /// 根据治疗单主键查询观察记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetListByVid(BaseInput input)
        {
            var visit = await _patVisitApp.GetForm(input.KeyValue);
            if (visit == null)
            {
                return BadRequest("治疗单主键有误");
            }
            var data = _dialysisObservationApp.GetListByVisit(input.KeyValue)
                .Select(t => new
                {
                    id = t.F_Id,
                    ssy = t.F_SSY,
                    szy = t.F_SZY,
                    hr = t.F_HR,
                    a = t.F_A,
                    bf = t.F_BF,
                    ufr = t.F_UFR,
                    v = t.F_V,
                    c = t.F_C,
                    t = t.F_T,
                    ufv = t.F_UFV,
                    tmp = t.F_TMP,
                    gsl = t.F_GSL,
                    memo = t.F_MEMO,
                    nurseName = t.F_Nurse,
                    operatorTime = t.F_NurseOperatorTime
                }).OrderBy(t=>t.operatorTime).ToList();
            return Ok(data);
        }

    }
}
