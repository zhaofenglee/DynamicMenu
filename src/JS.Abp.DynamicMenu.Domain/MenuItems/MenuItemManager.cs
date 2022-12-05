using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItemManager : DomainService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemManager(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<MenuItem> CreateAsync(
        Guid? parentId, string name, string displayName, bool isActive, string url, string icon, int order, string target, string elementId, string cssClass, string permission, string resourceTypeName)
        {
            var menuItem = new MenuItem(
             GuidGenerator.Create(),
             parentId, name, displayName, isActive, url, icon, order, target, elementId, cssClass, permission, resourceTypeName
             );

            return await _menuItemRepository.InsertAsync(menuItem);
        }

        public async Task<MenuItem> UpdateAsync(
            Guid id,
            Guid? parentId, string name, string displayName, bool isActive, string url, string icon, int order, string target, string elementId, string cssClass, string permission, string resourceTypeName, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _menuItemRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var menuItem = await AsyncExecuter.FirstOrDefaultAsync(query);

            menuItem.ParentId = parentId;
            menuItem.Name = name;
            menuItem.DisplayName = displayName;
            menuItem.IsActive = isActive;
            menuItem.Url = url;
            menuItem.Icon = icon;
            menuItem.Order = order;
            menuItem.Target = target;
            menuItem.ElementId = elementId;
            menuItem.CssClass = cssClass;
            menuItem.Permission = permission;
            menuItem.ResourceTypeName = resourceTypeName;

            menuItem.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _menuItemRepository.UpdateAsync(menuItem);
        }

    }
}