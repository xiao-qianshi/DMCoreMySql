using Dmt.DM.Code;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.ValueObject;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmt.DM.Application.PatientManage
{
    public interface ITreatmentApp : IScopedAppService
    {
        Task<List<TreatmentEntity>> GetList(Pagination pagination, string keyword);
        Task<IEnumerable<TreatmentSelectOptions>> GetList(string keyword = "");
        Task<TreatmentEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(TreatmentEntity entity);
        Task<int> SubmitForm<TDto>(TreatmentEntity entity, TDto dto) where TDto : class;
    }

    public class TreatmentApp : ITreatmentApp
    {
        private readonly IRepository<TreatmentEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IMemoryCache _memoryCache;

        public TreatmentApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<TreatmentEntity>();
            _httpContext = httpContext;
            _memoryCache = memoryCache;
        }

        public Task<List<TreatmentEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<TreatmentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_TreatmentCode.ToString().Contains(keyword));
                expression = expression.Or(t => t.F_TreatmentName.Contains(keyword));
                expression = expression.Or(t => t.F_TreatmentSpell.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<IEnumerable<TreatmentSelectOptions>> GetList(string keyword = "")
        {
            if (_memoryCache.TryGetValue("treatment_select_options", out List<TreatmentSelectOptions> cacheData))
                return string.IsNullOrEmpty(keyword)
                    ? Task.FromResult(cacheData.AsEnumerable())
                    : Task.FromResult(cacheData.Where(t =>
                        t.F_TreatmentCode.Contains(keyword) || t.F_TreatmentName.Contains(keyword) ||
                        t.F_TreatmentSpell.Contains(keyword)));
            {
                var expression = ExtLinq.True<TreatmentEntity>();
                expression = expression.And(t => t.F_EnabledMark == true);
                expression = expression.And(t => t.F_DeleteMark != true);
                cacheData = _service.IQueryable(expression).OrderBy(t => t.F_TreatmentName).Select(r => new TreatmentSelectOptions
                {
                    F_TreatmentCode = r.F_TreatmentCode,
                    F_TreatmentSpec = r.F_TreatmentSpec,
                    F_TreatmentName = r.F_TreatmentName,
                    F_TreatmentSpell = r.F_TreatmentSpell,
                    F_TreatmentUnit = r.F_TreatmentUnit,
                    F_Charges = r.F_Charges,
                    F_Id = r.F_Id
                }).ToList();
                _memoryCache.Set("treatment_select_options", cacheData, TimeSpan.FromMinutes(5));
            }

            return string.IsNullOrEmpty(keyword) ? Task.FromResult(cacheData.AsEnumerable()) : Task.FromResult(cacheData.Where(t =>
                t.F_TreatmentCode.Contains(keyword) || t.F_TreatmentName.Contains(keyword) || t.F_TreatmentSpell.Contains(keyword)));
        }
        
        public Task<TreatmentEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            //_service.Delete(t => t.F_Id == keyValue);
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(TreatmentEntity entity)
        {
            return _service.UpdateAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(TreatmentEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync(entity, dto);
            }
            else
            {
                entity.Create();
                entity.F_CreatorUserId = claim?.Value;
                return _service.InsertAsync(entity);
            }
        }

       
    }
}
