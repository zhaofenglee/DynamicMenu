using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.DynamicMenu.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MongoMenuItemRepository : MongoDbRepository<DynamicMenuMongoDbContext, MenuItem, Guid>, IMenuItemRepository
    {
        public MongoMenuItemRepository(IMongoDbContextProvider<DynamicMenuMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<MenuItem?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Name == name, GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<MenuItem>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? displayName = null,
            bool? isActive = null,
            string? url = null,
            string? icon = null,
            int? orderMin = null,
            int? orderMax = null,
            string? target = null,
            string? elementId = null,
            string? cssClass = null,
            string? permission = null,
            string? resourceTypeName = null,
            Guid? parentId = null,
            string? component = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync(cancellationToken)), filterText, name, displayName, isActive, url, icon, orderMin, orderMax, target, elementId, cssClass, permission, resourceTypeName, parentId,component);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MenuItemConsts.GetDefaultSorting(false) : sorting);
            return await query
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? displayName = null,
            bool? isActive = null,
            string? url = null,
            string? icon = null,
            int? orderMin = null,
            int? orderMax = null,
            string? target = null,
            string? elementId = null,
            string? cssClass = null,
            string? permission = null,
            string? resourceTypeName = null,
            Guid? parentId = null,
            string? component = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync(cancellationToken)), filterText, name, displayName, isActive, url, icon, orderMin, orderMax, target, elementId, cssClass, permission, resourceTypeName, parentId,component);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MenuItem> ApplyFilter(
            IQueryable<MenuItem> query,
            string? filterText = null,
            string? name = null,
            string? displayName = null,
            bool? isActive = null,
            string? url = null,
            string? icon = null,
            int? orderMin = null,
            int? orderMax = null,
            string? target = null,
            string? elementId = null,
            string? cssClass = null,
            string? permission = null,
            string? resourceTypeName = null,
            Guid? parentId = null,
            string? component = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.DisplayName!.Contains(filterText!) || e.Url!.Contains(filterText!) || e.Icon!.Contains(filterText!) || e.Target!.Contains(filterText!) || e.ElementId!.Contains(filterText!) || e.CssClass!.Contains(filterText!) || e.Permission!.Contains(filterText!) || e.ResourceTypeName!.Contains(filterText!)|| e.Component!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.DisplayName.Contains(displayName))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.Url.Contains(url))
                    .WhereIf(!string.IsNullOrWhiteSpace(icon), e => e.Icon.Contains(icon))
                    .WhereIf(orderMin.HasValue, e => e.Order >= orderMin!.Value)
                    .WhereIf(orderMax.HasValue, e => e.Order <= orderMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(target), e => e.Target.Contains(target))
                    .WhereIf(!string.IsNullOrWhiteSpace(elementId), e => e.ElementId.Contains(elementId))
                    .WhereIf(!string.IsNullOrWhiteSpace(cssClass), e => e.CssClass.Contains(cssClass))
                    .WhereIf(!string.IsNullOrWhiteSpace(permission), e => e.Permission.Contains(permission))
                    .WhereIf(!string.IsNullOrWhiteSpace(resourceTypeName), e => e.ResourceTypeName.Contains(resourceTypeName))
                    .WhereIf(parentId.HasValue, e => e.ParentId == parentId)
                    .WhereIf(!string.IsNullOrWhiteSpace(component), e => e.Component.Contains(component));
        }
    }
}