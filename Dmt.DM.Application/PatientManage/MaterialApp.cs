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
    public interface IMaterialApp : IScopedAppService
    {
        Task<List<MaterialEntity>> GetList(Pagination pagination, string keyword);
        Task<IEnumerable<MaterialSelectOptions>> GetList(string keyword = "");
        Task<IEnumerable<MaterialSelectOptions>> GetListByType(string keyword = "");
        /// <summary>
        /// 由于界面选择 透析器
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<IEnumerable<MaterialSelectOptions>> GetDialyzerList(string keyword = "");

        Task<MaterialEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> UpdateForm(MaterialEntity entity);
        Task<int> SubmitForm<TDto>(MaterialEntity entity, TDto dto) where TDto : class;
    }

    public class MaterialApp : IMaterialApp
    {
        private readonly IRepository<MaterialEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IMemoryCache _memoryCache = null;

        public MaterialApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<MaterialEntity>();
            _httpContext = httpContext;
            _memoryCache = memoryCache;
        }

        public Task<List<MaterialEntity>> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<MaterialEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_MaterialCode.Contains(keyword));
                expression = expression.Or(t => t.F_MaterialName.Contains(keyword));
                expression = expression.Or(t => t.F_MaterialSpell.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark != true);
            return _service.FindListAsync(expression, pagination);
        }

        public Task<IEnumerable<MaterialSelectOptions>> GetList(string keyword = "")
        {
            if (_memoryCache.TryGetValue("material_select_options", out List<MaterialSelectOptions> cacheData))
                return string.IsNullOrEmpty(keyword)
                    ? Task.FromResult(cacheData.AsEnumerable())
                    : Task.FromResult(cacheData.Where(t =>
                        t.F_MaterialCode.Contains(keyword) || t.F_MaterialName.Contains(keyword) ||
                        t.F_MaterialSpell.Contains(keyword)));
            {
                var expression = ExtLinq.True<MaterialEntity>();
                expression = expression.And(t => t.F_EnabledMark == true);
                expression = expression.And(t => t.F_DeleteMark != true);
                cacheData = _service.IQueryable(expression).OrderBy(t => t.F_MaterialName).Select(r =>
                    new MaterialSelectOptions
                    {
                        F_MaterialCode = r.F_MaterialCode,
                        F_MaterialName = r.F_MaterialName,
                        F_MaterialSpec = r.F_MaterialSpec,
                        F_MaterialSpell = r.F_MaterialSpell,
                        F_MaterialUnit = r.F_MaterialUnit,
                        F_Charges = r.F_Charges,
                        F_IsDialyzer = r.F_IsDialyzer,
                        F_MaterialType = r.F_MaterialType,
                        F_MaterialSupplier = r.F_MaterialSupplier,
                        F_Id = r.F_Id
                    }).ToList();
                _memoryCache.Set("material_select_options", cacheData, TimeSpan.FromMinutes(5));
            }

            return string.IsNullOrEmpty(keyword) ? Task.FromResult(cacheData.AsEnumerable()) : Task.FromResult(cacheData.Where(t =>
                t.F_MaterialCode.Contains(keyword) || t.F_MaterialName.Contains(keyword) || t.F_MaterialSpell.Contains(keyword)));
        }

        public async Task<IEnumerable<MaterialSelectOptions>> GetListByType(string keyword = "")
        {
            return string.IsNullOrEmpty(keyword)
                ? await GetList("")
                : (await GetList("")).Where(t => t.F_MaterialType==keyword);
        }

        /// <summary>
        /// 由于界面选择 透析器
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MaterialSelectOptions>> GetDialyzerList(string keyword = "")
        {
            return (await GetList(keyword)).Where(t => t.F_IsDialyzer == true);
        }

        public Task<MaterialEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            var entity = _service.FindEntity(keyValue);
            entity.F_DeleteMark = true;
            return UpdateForm(entity);
        }

        public Task<int> UpdateForm(MaterialEntity entity)
        {
            return _service.UpdatePartialAsync(entity);
        }

        public Task<int> SubmitForm<TDto>(MaterialEntity entity, TDto dto) where TDto : class
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
