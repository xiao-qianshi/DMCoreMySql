using AutoMapper;
using Dmt.DM.Application;
using Dmt.DM.Application.PatientManage;
using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.Dto;
using Dmt.DM.Mapper.Dto.PatientManage.Setting;
using Dmt.DM.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Web.Areas.PatientManage.Controllers
{
    [Area("PatientManage")]
    public class SettingController : BaseController
    {
        private readonly ISettingApp _settingApp;
        private readonly ISettingModelApp _settingModelApp;
        private readonly IMaterialApp _materialApp;
        private readonly IDrugsApp _drugsApp;
        private readonly IUsersService _userApp;
        private readonly IPatientApp _patientApp;
        private readonly IMapper _mapper;

        public SettingController(
            ISettingApp settingApp,
            ISettingModelApp settingModelApp,
            IMaterialApp materialApp,
            IDrugsApp drugsApp,
            IUsersService userApp,
            IPatientApp patientApp,
            IMapper mapper
        )
        {
            _settingApp = settingApp;
            _settingModelApp = settingModelApp;
            _materialApp = materialApp;
            _drugsApp = drugsApp;
            _userApp = userApp;
            _patientApp = patientApp;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await _settingApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        public async Task<IActionResult> GetSelectJson()
        {
            var data = await _settingApp.GetSelectList();
            return Content(data.ToJson());
        }


        public async Task<IActionResult> GetList(string keyword)
        {
            var list = (await _settingApp.GetSelectList(keyword)).Select(t => new
            {
                t.F_AccessName,
                t.F_BloodSpeed,
                t.F_Ca,
                t.F_CreatorTime,
                t.F_LastModifyTime,
                t.F_DialysateTemperature,
                t.F_DialysisType,
                t.F_DialyzerType1,
                //F_DialyzerTypeName1 = "",
                t.F_DialyzerType2,
                //F_DialyzerTypeName2 = "",
                t.F_DilutionType,
                t.F_EstimateHours,
                t.F_ExchangeAmount,
                t.F_ExchangeSpeed,
                t.F_Hco3,
                t.F_HeparinAddAmount,
                t.F_HeparinAddSpeedUnit,
                t.F_HeparinAmount,
                t.F_HeparinType,
                //F_HeparinTypeName = "",
                t.F_HeparinUnit,
                t.F_Id,
                t.F_IsDefault,
                t.F_K,
                t.F_LowCa,
                t.F_Na,
                t.F_Pid,
                t.F_VascularAccess,
                t.F_CreatorUserId
            }).ToList();

            var maritalIds = list.Where(t => t.F_DialyzerType1 != null).Select(t => t.F_DialyzerType1)
                .Concat(list.Where(t => t.F_DialyzerType2 != null).Select(t => t.F_DialyzerType2)).Distinct();
            var drugIds = list.Where(t => t.F_HeparinType != null).Select(t => t.F_HeparinType);
            var maritals = (await _materialApp.GetList()).Where(t => maritalIds.Contains(t.F_Id)).Select(t => new { t.F_Id, t.F_MaterialName }).ToList();
            var drugs = (await _drugsApp.GetSelectJson()).Where(t => drugIds.Contains(t.F_Id)).Select(t => new { t.F_Id, t.F_DrugName }).ToList();
            var users =  _userApp.GetUserNameDict(null).Select(t => new
            {
                t.F_Id,
                t.F_RealName
            });
            var data = list.Select(t => new
            {
                t.F_AccessName,
                t.F_BloodSpeed,
                t.F_Ca,
                F_CreatorTime = t.F_LastModifyTime ?? t.F_CreatorTime,
                t.F_DialysateTemperature,
                t.F_DialysisType,
                t.F_DialyzerType1,
                F_DialyzerTypeName1 = maritals.FirstOrDefault(m => m.F_Id == t.F_DialyzerType1)?.F_MaterialName ?? "",
                t.F_DialyzerType2,
                F_DialyzerTypeName2 = maritals.FirstOrDefault(m => m.F_Id == t.F_DialyzerType2)?.F_MaterialName ?? "",
                t.F_DilutionType,
                t.F_EstimateHours,
                t.F_ExchangeAmount,
                t.F_ExchangeSpeed,
                t.F_Hco3,
                t.F_HeparinAddAmount,
                t.F_HeparinAddSpeedUnit,
                t.F_HeparinAmount,
                t.F_HeparinType,
                F_HeparinTypeName = drugs.FirstOrDefault(m => m.F_Id.Equals(t.F_HeparinType))?.F_DrugName ?? "",
                t.F_HeparinUnit,
                t.F_Id,
                t.F_IsDefault,
                t.F_K,
                t.F_LowCa,
                t.F_Na,
                t.F_Pid,
                t.F_VascularAccess,
                F_DoctorName = users.FirstOrDefault(u => u.F_Id == t.F_CreatorUserId)?.F_RealName ?? ""
            });
            return Content(data.ToJson());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm([FromBody]BaseSubmitInput<SettingDto> input)
        {
            SettingEntity entity;
            if (input.KeyValue.IsEmpty())
            {
                entity = _mapper.Map<SettingEntity>(input.Entity);
            }
            else
            {
                entity = await _settingApp.GetForm(input.KeyValue);
            }
            entity.CheckArgumentIsNull(nameof(entity));
            await _settingApp.SubmitForm(entity, input.Entity);
            return Success("操作成功。");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteForm([FromBody]BaseInput input)
        {
            await _settingApp.DeleteForm(input.KeyValue);
            return Success("删除成功。");
        }

        #region 透析参数模板
        /// <summary>
        /// 根据现有透析参数主键复制至模板记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveModel([FromBody]BaseInput input)
        {
            var entity = await _settingApp.GetForm(input.KeyValue);
            if (entity == null)
            {
                return Error("未找到记录，ID：" + input.KeyValue);
            }
            else
            {
                if (entity.F_EnabledMark == false || entity.F_DeleteMark == true)
                {
                    return Error("记录已停用或删除，ID：" + input.KeyValue);
                }
                if (string.IsNullOrEmpty(entity.F_DialysisType))
                {
                    return Error("透析方式为空");
                }
                if (!string.IsNullOrEmpty(entity.F_HeparinType))
                {
                    var drug = await _drugsApp.GetForm(entity.F_HeparinType);
                    if (drug == null)
                    {
                        return Error("抗凝剂有误");
                    }
                }
                if (!string.IsNullOrEmpty(entity.F_DialyzerType1))
                {
                    var material = await _materialApp.GetForm(entity.F_DialyzerType1);
                    if (material == null)
                    {
                        return Error("透析器有误");
                    }
                }
                if (!string.IsNullOrEmpty(entity.F_DialyzerType2))
                {
                    var material = await _materialApp.GetForm(entity.F_DialyzerType2);
                    if (material == null)
                    {
                        return Error("灌流器有误");
                    }
                }
                var modelEntity = (await _settingModelApp.GetList(_userApp.GetCurrentUserId())).FirstOrDefault(t => t.F_DialysisType==entity.F_DialysisType) ??
                                  new SettingModelEntity();
                modelEntity.F_AccessName = entity.F_AccessName;
                modelEntity.F_BloodSpeed = entity.F_BloodSpeed;
                modelEntity.F_Ca = entity.F_Ca;
                modelEntity.F_DialysateTemperature = entity.F_DialysateTemperature;
                modelEntity.F_DialysisType = entity.F_DialysisType;
                modelEntity.F_DialyzerType1 = entity.F_DialyzerType1;
                modelEntity.F_DialyzerType2 = entity.F_DialyzerType2;
                modelEntity.F_DilutionType = entity.F_DilutionType;
                modelEntity.F_EstimateHours = entity.F_EstimateHours;
                modelEntity.F_ExchangeAmount = entity.F_ExchangeAmount;
                modelEntity.F_ExchangeSpeed = entity.F_ExchangeSpeed;
                modelEntity.F_Hco3 = entity.F_Hco3;
                modelEntity.F_HeparinAddAmount = entity.F_HeparinAddAmount;
                modelEntity.F_HeparinAddSpeedUnit = entity.F_HeparinAddSpeedUnit;
                modelEntity.F_HeparinAmount = entity.F_HeparinAmount;
                modelEntity.F_HeparinType = entity.F_HeparinType;
                modelEntity.F_HeparinUnit = entity.F_HeparinUnit;
                modelEntity.F_K = entity.F_K;
                modelEntity.F_LowCa = entity.F_LowCa;
                modelEntity.F_Na = entity.F_Na;
                modelEntity.F_VascularAccess = entity.F_VascularAccess;
                modelEntity.F_EnabledMark = true;
                modelEntity.F_DeleteMark = false;
                if (string.IsNullOrEmpty(modelEntity.F_Id))
                {
                    await _settingModelApp.SubmitForm(modelEntity, modelEntity.F_Id);
                }
                else
                {
                    await _settingModelApp.UpdateForm(modelEntity, true);
                }
            }
            return Success("已成功保存为模板。");
        }

        /// <summary>
        /// 通过模板快速生成记录
        /// </summary>
        /// <param name="keyValue">患者ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CopyModel([FromBody]BaseInput input)
        {
            var patient = await _patientApp.GetForm(input.KeyValue);
            if (patient == null) return Error("患者ID有误");
            //查询已有记录
            var list = await _settingApp.GetSelectList(input.KeyValue);
            //查询本医生透析参数模板 
            var source = await _settingModelApp.GetList(_userApp.GetCurrentUserId());
            if (source.Count == 0) return Success("无透析参数模板。");
            var count = 0;
            foreach (var item in source)
            {
                if (list.Count(t => t.F_DialysisType.Equals(item.F_DialysisType)) > 0)
                {
                    //已有这种透析模式透析参数
                    continue;
                }
                if (!string.IsNullOrEmpty(item.F_DialyzerType1))//查询透析器是否在用
                {
                    var m = await _materialApp.GetForm(item.F_DialyzerType1);
                    if (m == null || m.F_EnabledMark == false || m.F_DeleteMark == true)
                    {
                        //删除模板 跳过循环
                        await _settingModelApp.DeleteForm(item.F_Id);
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(item.F_DialyzerType2))//查询灌流器是否在用
                {
                    var m = await _materialApp.GetForm(item.F_DialyzerType2);
                    if (m == null || m.F_EnabledMark == false || m.F_DeleteMark == true)
                    {
                        //删除模板 跳过循环
                        await _settingModelApp.DeleteForm(item.F_Id);
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(item.F_HeparinType))//查询抗凝剂信息
                {
                    var drug = await _drugsApp.GetForm(item.F_HeparinType);
                    if (drug == null || drug.F_EnabledMark == false || drug.F_DeleteMark == true)
                    {
                        //删除模板 跳过循环
                        await _settingModelApp.DeleteForm(item.F_Id);
                        continue;
                    }
                }
                var entity = new SettingEntity
                {
                    F_AccessName = item.F_AccessName,
                    F_BloodSpeed = item.F_BloodSpeed,
                    F_Ca = item.F_Ca,
                    F_DialysateTemperature = item.F_DialysateTemperature,
                    F_DialysisType = item.F_DialysisType,
                    F_DialyzerType1 = item.F_DialyzerType1,
                    F_DialyzerType2 = item.F_DialyzerType2,
                    F_DilutionType = item.F_DilutionType,
                    F_EstimateHours = item.F_EstimateHours,
                    F_EnabledMark = true,
                    F_ExchangeAmount = item.F_ExchangeAmount,
                    F_ExchangeSpeed = item.F_ExchangeSpeed,
                    F_Hco3 = item.F_Hco3,
                    F_HeparinAddAmount = item.F_HeparinAddAmount,
                    F_HeparinAddSpeedUnit = item.F_HeparinAddSpeedUnit,
                    F_HeparinAmount = item.F_HeparinAmount,
                    F_HeparinType = item.F_HeparinType,
                    F_HeparinUnit = item.F_HeparinUnit,
                    F_IsDefault = false,
                    F_K = item.F_K,
                    F_LowCa = item.F_LowCa,
                    F_Na = item.F_Na,
                    F_Pid = input.KeyValue,
                    F_VascularAccess = item.F_VascularAccess
                };
                await _settingApp.SubmitForm(entity, new object());
                count++;
            }
            return Success("已添加" + count.ToString() + "条记录");
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> DisableForm([FromBody]BaseInput input)
        {
            var entity = await _settingApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = false;
            await _settingApp.UpdateForm(entity);
            return Success("启用成功。");
        }

        [HttpPost]
        public async Task<IActionResult> EnabledForm([FromBody]BaseInput input)
        {
            var entity = await _settingApp.GetForm(input.KeyValue);
            entity.F_EnabledMark = true;
            await _settingApp.UpdateForm(entity);
            return Success("启用成功。");
        }
    }
}
