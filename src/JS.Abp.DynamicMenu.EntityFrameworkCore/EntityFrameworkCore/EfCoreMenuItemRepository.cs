using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.DynamicMenu.MenuItems;

namespace JS.Abp.DynamicMenu.EntityFrameworkCore
{
    public class EfCoreMenuItemRepository : EfCoreRepository<IDynamicMenuDbContext, MenuItem, Guid>, IMenuItemRepository
    {
        public EfCoreMenuItemRepository(IDbContextProvider<IDynamicMenuDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<MenuItemWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(menuItem => new MenuItemWithNavigationProperties
                {
                    MenuItem = menuItem,
                    MenuItem1 = dbContext.MenuItems.FirstOrDefault(c => c.Id == menuItem.ParentId)
                }).FirstOrDefault();
        }

        public async Task<List<MenuItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, displayName, isActive, url, icon, orderMin, orderMax, target, elementId, cssClass, permission, resourceTypeName, parentId,component);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MenuItemConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<MenuItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from menuItem in await GetDbSetAsync()
                   join menuItem1 in (await GetDbContextAsync()).MenuItems on menuItem.ParentId equals menuItem1.Id into menuItems1
                   from menuItem1 in menuItems1.DefaultIfEmpty()

                   select new MenuItemWithNavigationProperties
                   {
                       MenuItem = menuItem,
                       MenuItem1 = menuItem1
                   };
        }

        protected virtual IQueryable<MenuItemWithNavigationProperties> ApplyFilter(
            IQueryable<MenuItemWithNavigationProperties> query,
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
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.MenuItem.Name.Contains(filterText) || e.MenuItem.DisplayName.Contains(filterText) || e.MenuItem.Url.Contains(filterText) || e.MenuItem.Icon.Contains(filterText) || e.MenuItem.Target.Contains(filterText) || e.MenuItem.ElementId.Contains(filterText) || e.MenuItem.CssClass.Contains(filterText) || e.MenuItem.Permission.Contains(filterText) || e.MenuItem.ResourceTypeName.Contains(filterText)||e.MenuItem.Component.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.MenuItem.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.MenuItem.DisplayName.Contains(displayName))
                    .WhereIf(isActive.HasValue, e => e.MenuItem.IsActive == isActive)
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.MenuItem.Url.Contains(url))
                    .WhereIf(!string.IsNullOrWhiteSpace(icon), e => e.MenuItem.Icon.Contains(icon))
                    .WhereIf(orderMin.HasValue, e => e.MenuItem.Order >= orderMin.Value)
                    .WhereIf(orderMax.HasValue, e => e.MenuItem.Order <= orderMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(target), e => e.MenuItem.Target.Contains(target))
                    .WhereIf(!string.IsNullOrWhiteSpace(elementId), e => e.MenuItem.ElementId.Contains(elementId))
                    .WhereIf(!string.IsNullOrWhiteSpace(cssClass), e => e.MenuItem.CssClass.Contains(cssClass))
                    .WhereIf(!string.IsNullOrWhiteSpace(permission), e => e.MenuItem.Permission.Contains(permission))
                    .WhereIf(!string.IsNullOrWhiteSpace(resourceTypeName), e => e.MenuItem.ResourceTypeName.Contains(resourceTypeName))
                    .WhereIf(parentId != null && parentId != Guid.Empty, e => e.MenuItem1 != null && e.MenuItem1.Id == parentId)
                    .WhereIf(!string.IsNullOrWhiteSpace(component), e => e.MenuItem.Component.Contains(component));
        }

        public virtual async Task<MenuItem?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(b => b.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<List<MenuItem>> GetListAsync(
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
            var query = ApplyFilter(await GetQueryableAsync(), filterText, name, displayName, isActive, url, icon, orderMin, orderMax, target, elementId, cssClass, permission, resourceTypeName,parentId,component);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MenuItemConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, name, displayName, isActive, url, icon, orderMin, orderMax, target, elementId, cssClass, permission, resourceTypeName, parentId,component);
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
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.DisplayName.Contains(filterText) || e.Url.Contains(filterText) || e.Icon.Contains(filterText) || e.Target.Contains(filterText) || e.ElementId.Contains(filterText) || e.CssClass.Contains(filterText) || e.Permission.Contains(filterText) || e.ResourceTypeName.Contains(filterText) || e.Component.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.DisplayName.Contains(displayName))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive)
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.Url.Contains(url))
                    .WhereIf(!string.IsNullOrWhiteSpace(icon), e => e.Icon.Contains(icon))
                    .WhereIf(orderMin.HasValue, e => e.Order >= orderMin.Value)
                    .WhereIf(orderMax.HasValue, e => e.Order <= orderMax.Value)
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