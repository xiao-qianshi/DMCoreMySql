using System;
using Dmt.DM.Code;
using Dmt.DM.Mapper.Dto.PatientManage.Drugs;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dmt.DM.Domain.Entity.PatientManage;
using Dmt.DM.Mapper.ValueObject;
using Microsoft.Extensions.Caching.Memory;

namespace Dmt.DM.Application.PatientManage
{
    public interface IDrugsApp : IScopedAppService
    {
        Task<List<DrugsEntity>> GetList(Pagination pagination, string keyword);
        Task<IEnumerable<DrugsSelectOptions>> GetList(string keyword = "");

        /// <summary>
        /// 由于界面选择 透析药品 肝素
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<IEnumerable<DrugsSelectOptions>> GetSelectJson(string keyword = "");

        Task<DrugsEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(DrugsEntity entity);
        Task<int> SubmitForm<TDto>(DrugsEntity entity, TDto dto) where TDto : class;
    }

    public class DrugsApp : IDrugsApp
    {
        private readonly IRepository<DrugsEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IMemoryCache _memoryCache = null;

        public DrugsApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<DrugsEntity>();
            _httpContext = httpContext;
            _memoryCache = memoryCache;
        }

        public Task<List<DrugsEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<DrugsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_DrugCode.Contains(keyword));
                expression = expression.Or(t => t.F_DrugName.Contains(keyword));
                expression = expression.Or(t => t.F_DrugSpell.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }
 
        public Task<IEnumerable<DrugsSelectOptions>> GetList(string keyword = "")
        {
            if (_memoryCache.TryGetValue("drugs_select_options", out List<DrugsSelectOptions> cacheData))
                return string.IsNullOrEmpty(keyword)
                    ? Task.FromResult(cacheData.AsEnumerable())
                    : Task.FromResult(cacheData.Where(t =>
                        t.F_DrugCode.Contains(keyword) || t.F_DrugName.Contains(keyword) ||
                        t.F_DrugSpell.Contains(keyword)));
            {
                var expression = ExtLinq.True<DrugsEntity>();
                expression = expression.And(t => t.F_EnabledMark == true);
                expression = expression.And(t => t.F_DeleteMark != true);
                cacheData = _service.IQueryable(expression).OrderBy(t => t.F_DrugName).Select(r => new DrugsSelectOptions
                {
                    F_DrugAdministration = r.F_DrugAdministration,
                    F_DrugCode = r.F_DrugCode,
                    F_DrugMiniAmount = r.F_DrugMiniAmount,
                    F_DrugMiniPackage = r.F_DrugMiniPackage,
                    F_DrugMiniSpec = r.F_DrugMiniSpec,
                    F_DrugName = r.F_DrugName,
                    F_DrugSpec = r.F_DrugSpec,
                    F_DrugSpell = r.F_DrugSpell,
                    F_DrugSupplier = r.F_DrugSupplier,
                    F_DrugUnit = r.F_DrugUnit,
                    F_Charges = r.F_Charges,
                    F_IsHeparin = r.F_IsHeparin,
                    F_Id = r.F_Id
                }).ToList();
                _memoryCache.Set("drugs_select_options", cacheData, TimeSpan.FromMinutes(5));
            }

            return string.IsNullOrEmpty(keyword)? Task.FromResult(cacheData.AsEnumerable()) : Task.FromResult(cacheData.Where(t =>
                t.F_DrugCode.Contains(keyword) || t.F_DrugName.Contains(keyword) || t.F_DrugSpell.Contains(keyword)));
        }


        /// <summary>
        /// 由于界面选择 透析药品 肝素
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DrugsSelectOptions>> GetSelectJson(string keyword = "")
        {
            return (await GetList(keyword)).Where(t => t.F_IsHeparin == true);
        }

        public Task<DrugsEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(DrugsEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }
        
        public Task<int> SubmitForm<TDto>(DrugsEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            claimsIdentity.CheckArgumentIsNull(nameof(claimsIdentity));
            var claim = claimsIdentity?.FindFirst(t => t.Type==ClaimTypes.NameIdentifier);

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
