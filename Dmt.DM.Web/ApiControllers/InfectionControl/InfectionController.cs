using Dmt.Dm.Domain.Dto.InfectionControl.Infection;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.ApiControllers.InfectionControl
{
    [Route("api/[controller]/[action]")]
    public class InfectionController : BaseApiController
    {
        private readonly IInfectionApp _infectionApp;
        private readonly IUsersService _usersService;

        public InfectionController(IInfectionApp infectionApp, IUsersService usersService)
        {
            _infectionApp = infectionApp;
            _usersService = usersService;
        }

        /// <summary>
        /// 根据日期段分页查询记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPagedList(GetPagedListInput input)
        {
            var pagination = new Pagination
            {
                rows = input.rows,
                page = input.page,
                sidx = input.orderField ?? "F_ReportDate",
                sord = input.orderType
            };
            var users = _usersService.GetUserNameDict("").Select(t => new
            {
                id = t.F_Id,
                name = t.F_RealName
            }).ToList();
            var list = (await _infectionApp.GetList(pagination, input.startDate.ToDate(), input.endDate.ToDate()))
                .Select(t => new
                {
                    id = t.F_Id,
                    reportDate = t.F_ReportDate,
                    item1 = t.F_Item1.ToFloat(2),
                    item2 = t.F_Item2.ToFloat(2),
                    item3 = t.F_Item3.ToFloat(2),
                    item4 = t.F_Item4.ToFloat(2),
                    item5 = t.F_Item5.ToFloat(2),
                    item6 = t.F_Item6.ToFloat(2),
                    item7 = t.F_Item7.ToFloat(2),
                    recordPerson = t.F_RecordPerson == null ? "" : users.First(u => u.id.Equals(t.F_RecordPerson)).name,
                    memo = t.F_Memo
                });

            var source = list.OrderByDescending(t => t.reportDate).Select(t => new
            {
                t.id,
                t.recordPerson,
                t.reportDate,
                t.item1,
                t.item2,
                t.item3,
                t.item4,
                t.item5,
                t.item6,
                t.item7,
                isAbnormal = t.item1 > 4 || t.item2 > 10 || t.item3 > 10 || t.item4 > 100 || t.item5 > 0.25 || t.item6 > 100 || t.item7 > 0.25
            }).ToList();

            var data = new
            {
                rows = source,
                abnormalCount = source.Count(t => t.isAbnormal == true),
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Ok(data);
        }

        public async Task<IActionResult> GetForm(BaseInput input)
        {
            var entity = await _infectionApp.GetForm(input.KeyValue);
            var data = new
            {
                id = entity.F_Id,
                reportDate = entity.F_ReportDate,
                item1 = entity.F_Item1.ToFloat(2),
                item2 = entity.F_Item2.ToFloat(2),
                item3 = entity.F_Item3.ToFloat(2),
                item4 = entity.F_Item4.ToFloat(2),
                item5 = entity.F_Item5.ToFloat(2),
                item6 = entity.F_Item6.ToFloat(2),
                item7 = entity.F_Item7.ToFloat(2),
                recordPerson = entity.F_RecordPerson == null ? "" : (await _usersService.FindUserAsync(entity.F_RecordPerson))?.F_RealName,
                imangePath = entity.F_ImangePath,
                memo = entity.F_Memo
            };

            return Ok(data);
        }
        /// <summary>
        /// 新增修改记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitData([FromBody]SubmitDataInput input)
        {
            var userId = _usersService.GetCurrentUserId();
            InfectionEntity entity = null;
            if (string.IsNullOrEmpty(input.id))//新建
            {
                entity = new InfectionEntity
                {
                    F_Id = Common.GuId(),
                    F_RecordPerson = userId,
                    F_CreatorTime = DateTime.Now,
                    F_CreatorUserId = userId,
                    F_EnabledMark = true
                };
            }
            else//修改
            {
                entity = new InfectionEntity
                {
                    F_Id = input.id,
                    F_LastModifyTime = DateTime.Now,
                    F_LastModifyUserId = userId
                };
            }
            entity.F_Item1 = input.item1;
            entity.F_Item2 = input.item2;
            entity.F_Item3 = input.item3;
            entity.F_Item4 = input.item4;
            entity.F_Item5 = input.item5;
            entity.F_Item6 = input.item6;
            entity.F_Item7 = input.item7;

            if (string.IsNullOrEmpty(input.id))
            {
                _infectionApp.InsertForm(entity);
            }
            else
            {
                _infectionApp.UpdateForm(entity);
            }
            var data = new
            {
                id = entity.F_Id
            };
            return Ok(data);
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadImage(IFormFile file, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("未传值【id】！");
            }
            var targetPath = Path.Combine(AppConsts.AppRootPath, "upload", "Infection");
            FileHelper.CreateDirectory(targetPath);
            var serialNo = Common.CreateNo();
            var fileName = Path.Combine(targetPath, serialNo + Path.GetExtension(file.FileName));
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }

            var entity = await _infectionApp.GetForm(id);
            entity.F_ImangePath = fileName.Replace(AppConsts.AppRootPath, "").Replace("\\", "/").TrimStart('/');
            await _infectionApp.UpdateForm(entity);
            return Ok("操作成功");
        }
    }
}
