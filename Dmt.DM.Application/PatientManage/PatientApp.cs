using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.ValueObject;
using Dmt.DM.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface IPatientApp : IScopedAppService
    {
        Task<List<PatientEntity>> GetList(Pagination pagination, string keyword);
        //Task<List<PatientEntity>> GetList();
        Task<List<PatientSelectOptions>> GetSelectOptions(string keyValue);
      
        /// <summary>
        /// 查询转归记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<List<TransferLogEntity>> GetTransferList(string keyValue);

        IQueryable<PatientEntity> GetQueryable();

        /// <summary>
        /// 随机取一个患者
        /// </summary>
        /// <returns></returns>
        Task<PatientEntity> GetRandom();

        Task<List<PatientEntity>> GetList(string keyValue = "");

        /// <summary>
        /// 根据 透析号/病历号/姓名/拼音 查询
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<List<PatientEntity>> GetListFilter(string keyValue = "");

        Task<float> GetIdeaWeight(string keyValue);
        Task<PatientEntity> GetForm(string keyValue);
        Task<PatientEntity> GetFormById(string dialysisNo);
        Task<int> DeleteForm(string keyValue);
        //Task<int> UpdateForm(PatientEntity userEntity, bool isSelf = false);
        Task<int> UpdateForm(PatientEntity userEntity);
        /// <summary>
        /// 因App新增
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        Task<int> UpdateWeight(string pid, string userId, float value);

        //Task SubmitForm(PatientEntity patientEntity, string keyValue);
        Task<int> DeleteTransfer(string keyValue);
        Task<int> SubmitTransfer(TransferLogEntity entity, string keyValue);
        Task<int> GetCount();
        Task<int> SubmitForm<TDto>(PatientEntity patientEntity, TDto inputEntity) where TDto : class;
    }

    public class PatientApp : IPatientApp
    {
        private readonly IRepository<PatientEntity> _service = null;
        private readonly ITransferLogApp _tranferLogApp = null;
        private readonly IUnitOfWork _uow = null;
        //private readonly IHttpContextAccessor _httpContext = null;
        private readonly IUsersService _usersService = null;
        private readonly IMemoryCache _memoryCache = null;

        public PatientApp(IUnitOfWork uow, IUsersService usersService, ITransferLogApp transferLogApp,IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<PatientEntity>();
            _usersService = usersService;
            _memoryCache = memoryCache;
            _tranferLogApp = transferLogApp;
        }

        public Task<List<PatientEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<PatientEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                if (int.TryParse(keyword, out int dialysisNo))
                {
                    expression = expression.And(t => t.F_DialysisNo == dialysisNo)
                        .Or(t => t.F_Name.Contains(keyword))
                        .Or(t => t.F_RecordNo == keyword)
                        .Or(t => t.F_PatientNo == keyword)
                        .Or(t => t.F_PY.Contains(keyword));
                }
                else
                {
                    expression = expression.And(t => t.F_Name.Contains(keyword))
                        .Or(t => t.F_RecordNo == keyword)
                        .Or(t => t.F_PatientNo == keyword)
                        .Or(t => t.F_PY.Contains(keyword));
                }

            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<List<PatientSelectOptions>> GetSelectOptions(string keyValue)
        {
            List<PatientSelectOptions> list;
            if (string.IsNullOrEmpty(keyValue))
            {
                if (_memoryCache.TryGetValue("patient_select_options", out list)) return Task.FromResult(list);
                list = GetQueryable().Select(r => new PatientSelectOptions
                {
                    Id = r.F_Id,
                    Name = r.F_Name,
                    RecordNo = r.F_DialysisNo,
                    Py = r.F_PY,
                    IdealWeight = r.F_IdealWeight,
                    BeInfected = "+".Equals(r.F_Tp) || "+".Equals(r.F_Hiv) || "+".Equals(r.F_HBsAg) || "+".Equals(r.F_HBeAg) || "+".Equals(r.F_HBeAb)
                }).ToList();
                if (list.Any()) _memoryCache.Set("patient_select_options", list, TimeSpan.FromMinutes(10));

            }
            else
            {
                list = GetQueryable().Where(t => t.F_Id == keyValue).Select(r => new PatientSelectOptions
                {
                    Id = r.F_Id,
                    Name = r.F_Name,
                    RecordNo = r.F_DialysisNo,
                    Py = r.F_PY,
                    IdealWeight = r.F_IdealWeight,
                    BeInfected = "+".Equals(r.F_Tp) || "+".Equals(r.F_Hiv) || "+".Equals(r.F_HBsAg) ||
                                 "+".Equals(r.F_HBeAg) || "+".Equals(r.F_HBeAb)
                }).ToList();
            }
            return Task.FromResult(list);
        }

        /// <summary>
        /// 查询转归记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<List<TransferLogEntity>> GetTransferList(string keyValue)
        {
            return await _tranferLogApp.GetList(keyValue);
        }

        public IQueryable<PatientEntity> GetQueryable()
        {
            var expression = ExtLinq.True<PatientEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression);
        }

        /// <summary>
        /// 随机取一个患者
        /// </summary>
        /// <returns></returns>
        public Task<PatientEntity> GetRandom()
        {
            var expression = ExtLinq.True<PatientEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);
            return _service.IQueryable(expression).FirstOrDefaultAsync();
        }

        public Task<List<PatientEntity>> GetList(string keyValue = "")
        {
            var expression = ExtLinq.True<PatientEntity>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_Id == keyValue);
            }
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        /// <summary>
        /// 根据 透析号/病历号/姓名/拼音 查询
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public Task<List<PatientEntity>> GetListFilter(string keyValue = "")
        {
            var expression = ExtLinq.True<PatientEntity>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (int.TryParse(keyValue, out int dialysisNo))
                {
                    expression = expression.And(t => t.F_Name.Contains(keyValue))
                    .Or(t => t.F_RecordNo.Contains(keyValue))
                    .Or(t => t.F_DialysisNo == dialysisNo)
                    .Or(t => t.F_PY.Contains(keyValue.ToUpper()));
                }
                else
                {
                    expression = expression.And(t => t.F_Name.Contains(keyValue))
                       .Or(t => t.F_RecordNo.Contains(keyValue))
                       .Or(t => t.F_PY.Contains(keyValue.ToUpper()));
                }

            }
            expression = expression.And(t => t.F_EnabledMark != false);
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).ToListAsync();
        }

        public async Task<float> GetIdeaWeight(string keyValue)
        {
            var entity = await _service.FindEntityAsync(keyValue);
            var value = entity?.F_IdealWeight;
            return value?.ToFloat(2) ?? 0f;
            //if (string.IsNullOrEmpty(keyValue)) return Task.FromResult(weight);
            //var expression = ExtLinq.True<PatientEntity>();
            //expression = expression.And(t => t.F_Id.Equals(keyValue));
            //expression = expression.And(t => t.F_EnabledMark != false);
            //expression = expression.And(t => t.F_DeleteMark != true);
            //var entity = _service.IQueryable(expression).Select(t => t.F_IdealWeight).FirstOrDefault();
            //weight = entity.ToFloat(2);
            //return Task.FromResult(weight);
        }

        public Task<PatientEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }

        public Task<PatientEntity> GetFormById(string dialysisNo)
        {
            var expression = ExtLinq.True<PatientEntity>();
            if (!string.IsNullOrEmpty(dialysisNo))
            {
                expression = int.TryParse(dialysisNo, out var tempInt)
                    ? expression.And(t => t.F_DialysisNo == tempInt).Or(t => t.F_CardNo == dialysisNo)
                        .Or(t => t.F_Id == dialysisNo)
                    : expression.And(t => t.F_CardNo == dialysisNo).Or(t => t.F_Id == dialysisNo);
            }
            else
            {
                expression = expression.And(t => 1 == 0);
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.IQueryable(expression).FirstOrDefaultAsync();
        }

        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        //public Task<int> UpdateForm(PatientEntity userEntity, bool isSelf = false)
        //{
        //    return _service.UpdateAsync(userEntity, isSelf);
        //}

        public Task<int> UpdateForm(PatientEntity userEntity)
        {
            return _service.UpdatePartialAsync(userEntity);
        }
        /// <summary>
        /// 因App新增
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        public Task<int> UpdateWeight(string pid, string userId, float value)
        {
            var patient = _service.FindEntity(pid);
            if (patient == null) return Task.FromResult(0);
            patient.F_IdealWeight = value;
            _service.Update(patient, true);
            //查询体重修改记录
            var ideaWeightRepository = _uow.GetRepository<IdeaWeightEntity>();
            var expression = ExtLinq.True<IdeaWeightEntity>();
            expression = expression.And(t => t.F_Pid == pid).And(t => t.F_EnabledMark == true);
            var query = ideaWeightRepository.IQueryable(expression);
            var entity = query.OrderByDescending(t => t.F_CreatorTime).FirstOrDefault();
            if (entity != null && Math.Abs(entity.F_IdealWeight.ToFloat(2) - value) < 0.01) return Task.FromResult(0);
            var newentity = new IdeaWeightEntity
            {
                F_Id = Common.GuId(),
                F_Pid = patient.F_Id,
                F_Name = patient.F_Name,
                F_DialysisNo = patient.F_DialysisNo,
                F_IdealWeight = value,
                F_EnabledMark = true,
                F_CreatorUserId = userId,
                F_CreatorTime = DateTime.Now
            };
            if (entity != null)
            {
                newentity.F_OldIdealWeight = entity.F_IdealWeight;
            }
            return ideaWeightRepository.InsertAsync(newentity);
        }

        //public Task SubmitForm(PatientEntity patientEntity, string keyValue)
        //{
        //    var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
        //    claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
        //    var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
        //    if (!string.IsNullOrEmpty(keyValue))
        //    {
        //        patientEntity.Modify(keyValue);
        //        patientEntity.F_LastModifyUserId = claim?.Value;
        //        _service.Update(patientEntity);
        //    }
        //    else
        //    {
        //        patientEntity.Create();
        //        patientEntity.F_CreatorUserId = claim?.Value;
        //        _service.Insert(patientEntity);
        //    }

        //    if (patientEntity.F_IdealWeight != null)
        //    {
        //        var newValue = patientEntity.F_IdealWeight.ToFloat(2);
        //        var ideaWeightRepository = _uow.GetRepository<IdeaWeightEntity>();
        //        var expression = ExtLinq.True<IdeaWeightEntity>();
        //        expression = expression.And(t => t.F_Pid.Equals(patientEntity.F_Id))
        //            .And(t => t.F_EnabledMark == true);
        //        var ideaWeightEntity = ideaWeightRepository.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime).FirstOrDefault();
        //        if (ideaWeightEntity == null)
        //        {
        //            ideaWeightEntity = new IdeaWeightEntity
        //            {
        //                F_Pid = patientEntity.F_Id,
        //                F_Name = patientEntity.F_Name,
        //                F_EnabledMark = true,
        //                F_DialysisNo = patientEntity.F_DialysisNo,
        //                F_IdealWeight = newValue
        //            };
        //            ideaWeightEntity.Create();
        //            ideaWeightRepository.Insert(ideaWeightEntity);
        //        }
        //        else
        //        {
        //            if (ideaWeightEntity.F_IdealWeight != null)
        //            {
        //                var oldValue = ideaWeightEntity.F_IdealWeight.ToFloat(2);
        //                if (oldValue != newValue)
        //                {
        //                    ideaWeightEntity = new IdeaWeightEntity
        //                    {
        //                        F_Pid = patientEntity.F_Id,
        //                        F_Name = patientEntity.F_Name,
        //                        F_EnabledMark = true,
        //                        F_DialysisNo = patientEntity.F_DialysisNo,
        //                        F_IdealWeight = newValue,
        //                        F_OldIdealWeight = oldValue
        //                    };
        //                    ideaWeightEntity.Create();
        //                    ideaWeightRepository.Insert(ideaWeightEntity);
        //                }
        //            }
        //        }
        //    }

        //    return Task.CompletedTask;
        //}

        public Task<int> DeleteTransfer(string keyValue)
        {
            return _tranferLogApp.DeleteForm(keyValue);
        }

        public Task<int> SubmitTransfer(TransferLogEntity entity, string keyValue)
        {
            return _tranferLogApp.SubmitForm(entity, keyValue);
        }

        public  Task<int> GetCount()
        {
            var expression = ExtLinq.True<PatientEntity>();
            expression = expression.And(t => t.F_DeleteMark != true);
            expression = expression.And(t => t.F_EnabledMark != false);
            return Task.FromResult(_service.IQueryable(expression).Count());
        }

        public async Task<int> SubmitForm<TDto>(PatientEntity entity, TDto dto) where TDto : class
        {
            var result = 0;
            //var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            //claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            //var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = _usersService.GetCurrentUserId();
                result = await _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = _usersService.GetCurrentUserId();
                result = await _service.InsertAsync(entity);
            }

            await AddIdeaWeightRecord(entity.F_Id);
            return result;
        }
        /// <summary>
        /// 添加干体重变化记录
        /// </summary>
        /// <param name="pid">患者ID</param>
        /// <returns></returns>
        private async Task AddIdeaWeightRecord(string pid)
        {
            var patientEntity = await _service.FindEntityAsync(pid);
            if (patientEntity.F_IdealWeight.HasValue)
            {
                var newValue = patientEntity.F_IdealWeight.ToFloat(2);
                var ideaWeightRepository = _uow.GetRepository<IdeaWeightEntity>();
                var expression = ExtLinq.True<IdeaWeightEntity>();
                expression = expression.And(t => t.F_Pid == patientEntity.F_Id)
                    .And(t => t.F_EnabledMark == true);
                var ideaWeightEntity = ideaWeightRepository.IQueryable(expression).OrderByDescending(t => t.F_CreatorTime)
                    .FirstOrDefault();
                if (ideaWeightEntity == null)
                {
                    ideaWeightEntity = new IdeaWeightEntity
                    {
                        F_Pid = patientEntity.F_Id,
                        F_Name = patientEntity.F_Name,
                        F_EnabledMark = true,
                        F_DialysisNo = patientEntity.F_DialysisNo,
                        F_IdealWeight = newValue
                    };
                    ideaWeightEntity.Create();
                    await ideaWeightRepository.InsertAsync(ideaWeightEntity);
                }
                else
                {
                    if (ideaWeightEntity.F_IdealWeight != null)
                    {
                        var oldValue = ideaWeightEntity.F_IdealWeight.ToFloat(2);
                        if (Math.Abs(oldValue - newValue) > 0.001f)
                        {
                            ideaWeightEntity = new IdeaWeightEntity
                            {
                                F_Pid = patientEntity.F_Id,
                                F_Name = patientEntity.F_Name,
                                F_EnabledMark = true,
                                F_DialysisNo = patientEntity.F_DialysisNo,
                                F_IdealWeight = newValue,
                                F_OldIdealWeight = oldValue
                            };
                            ideaWeightEntity.Create();
                            await ideaWeightRepository.InsertAsync(ideaWeightEntity);
                        }
                    }
                }
            }
        }
    }
}
