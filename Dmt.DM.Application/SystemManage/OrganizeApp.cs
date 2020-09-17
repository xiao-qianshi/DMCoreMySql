using Dmt.DM.Domain.Entity.SystemManage;
using Dmt.DM.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dmt.DM.Code;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

namespace Dmt.DM.Application.SystemManage
{
    public interface IOrganizeApp : IScopedAppService
    {
        Task<List<OrganizeEntity>> GetList();
        Task<OrganizeEntity> GetForm(string keyValue);
        Task<int> DeleteForm(string keyValue);
        Task<int> SubmitForm(OrganizeEntity organizeEntity, string keyValue);
        Task<int> SubmitForm<TDto>(OrganizeEntity entity, TDto dto) where TDto : class;
        /// <summary>
        /// 获取医院名称
        /// </summary>
        /// <returns></returns>
        Task<string> GetHospitalName();

        /// <summary>
        /// 获取医院 Logo byte[]
        /// </summary>
        /// <returns></returns>
        Task<byte[]> GetHospitalLogo();
    }

    public class OrganizeApp : IOrganizeApp
    {
        private readonly IRepository<OrganizeEntity> _service = null;
        private readonly IUnitOfWork _uow = null;
        private readonly IHttpContextAccessor _httpContext = null;
        private readonly IMemoryCache _memoryCache = null;
        public OrganizeApp(IUnitOfWork uow, IHttpContextAccessor httpContext, IMemoryCache memoryCache)
        {
            _uow = uow;
            _service = _uow.GetRepository<OrganizeEntity>();
            _httpContext = httpContext;
            _memoryCache = memoryCache;
        }
        public Task<List<OrganizeEntity>> GetList()
        {
            return _service.IQueryable().OrderBy(t => t.F_CreatorTime).ToListAsync();
        }
        public Task<OrganizeEntity> GetForm(string keyValue)
        {
            return _service.FindEntityAsync(keyValue);
        }
        public Task<int> DeleteForm(string keyValue)
        {
            if (_service.IQueryable().Count(t => t.F_ParentId==keyValue) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                return _service.DeleteAsync(t => t.F_Id == keyValue);
            }
        }
        public Task<int> SubmitForm(OrganizeEntity organizeEntity, string keyValue)
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                if (claim != null) organizeEntity.F_LastModifyUserId = claim.Value;
                return _service.UpdateAsync(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                if (claim != null) organizeEntity.F_CreatorUserId = claim.Value;
                return _service.InsertAsync(organizeEntity);
            }
        }
        /// <summary>
        /// 获取医院名称
        /// </summary>
        /// <returns></returns>
        public Task<string> GetHospitalName()
        {
            return _service.IQueryable().Where(t => t.F_ParentId == "0").Select(t => t.F_FullName)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取医院 Logo byte[]
        /// </summary>
        /// <returns></returns>
        public Task<byte[]> GetHospitalLogo()
        {
            if (!_memoryCache.TryGetValue("Hospital_Logo_Data", out byte[] data))
            {
                var fileName = Path.Combine(AppConsts.AppRootPath, "wwwroot", "Content", "img", "logo_black.png");
                if (FileHelper.IsExistFile(fileName))
                {
                    data = FileHelper.GetFileData(fileName);
                    _memoryCache.Set("Hospital_Logo_Data", data, TimeSpan.FromHours(2));
                }
            }
            return Task.FromResult(data);
        }

        public Task<int> SubmitForm<TDto>(OrganizeEntity entity, TDto dto) where TDto : class
        {
            var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(t => t.Type.Equals(ClaimTypes.NameIdentifier));

            if (!string.IsNullOrEmpty(entity.F_Id))
            {
                entity.Modify(entity.F_Id);
                entity.F_LastModifyUserId = claim?.Value;
                return _service.UpdateAsync<TDto>(entity, dto);
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
