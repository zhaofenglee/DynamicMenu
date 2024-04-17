using JS.Abp.DynamicMenu.Shared;
using JS.Abp.DynamicMenu.MenuItems;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JS.Abp.DynamicMenu.Permissions;
using JS.Abp.DynamicMenu.MenuItems;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.DynamicMenu.Shared;
using Volo.Abp.Authorization.Permissions;

namespace JS.Abp.DynamicMenu.MenuItems
{
    [RemoteService(IsEnabled = false)]
    //[Authorize(DynamicMenuPermissions.MenuItems.Default)]
    public class MenuItemsAppService : ApplicationService, IMenuItemsAppService
    {
        private readonly IDistributedCache<MenuItemExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly MenuItemManager _menuItemManager;
        private readonly IDynamicPermissionDefinitionStore _dynamicStore;
        public MenuItemsAppService(IMenuItemRepository menuItemRepository,
            MenuItemManager menuItemManager, 
            IDistributedCache<MenuItemExcelDownloadTokenCacheItem, string> excelDownloadTokenCache,
            IDynamicPermissionDefinitionStore dynamicStore)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _menuItemRepository = menuItemRepository;
            _menuItemManager = menuItemManager;
            _dynamicStore = dynamicStore;
        }

        public virtual async Task<PagedResultDto<MenuItemDto>> GetListAsync(GetMenuItemsInput input)
        {
            var totalCount = await _menuItemRepository.GetCountAsync(input.FilterText, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.OrderMin, input.OrderMax, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName, input.ParentId);
            var items = await _menuItemRepository.GetListAsync(input.FilterText, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.OrderMin, input.OrderMax, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MenuItemDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(items)
            };
        }


        public virtual async Task<List<MenuItemTreeDto>> GetTreeAsync(string parentId = null)
        {
            var menuItemDatas = (await _menuItemRepository.GetListAsync());
            var nodes = new List<MenuItemTreeDto>();
            LoadRoot(nodes,menuItemDatas);
            return new List<MenuItemTreeDto>()
            {
                new MenuItemTreeDto()
                {
                    Id = null,
                    Name = "All",
                    DisplayName = "All",
                    Children = nodes
                },
            };
        }
        
        private void LoadRoot(List<MenuItemTreeDto> list, List<MenuItem> items)
        {
            foreach (var cacheGroup in items.Where(c => c.ParentId == null).OrderBy(c => c.Order))
            {
                MenuItemTreeDto pnode = ObjectMapper.Map<MenuItem, MenuItemTreeDto>(cacheGroup);
                LoadChild(pnode, items);
                list.Add(pnode);
            }
        }

        private void LoadChild(MenuItemTreeDto parentNode, List<MenuItem> items)
        {
            var children = items.Where(x => x.ParentId == parentNode.Id).ToList();
            foreach (var child in children)
            {
                var childNode = ObjectMapper.Map<MenuItem, MenuItemTreeDto>(child);
                parentNode.Children.Add(childNode);
                LoadChild(childNode, items);
            }
        }

        public virtual async Task<MenuItemDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MenuItem, MenuItemDto>(await _menuItemRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetMenuItemLookupAsync(LookupRequestDto input)
        {
            var query = (await _menuItemRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.DisplayName != null &&
                         x.DisplayName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<MenuItem>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MenuItem>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(DynamicMenuPermissions.MenuItems.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            //TODO: Check for dependencies before deletion
            await _menuItemRepository.DeleteAsync(id);
        }

        [Authorize(DynamicMenuPermissions.MenuItems.Create)]
        public virtual async Task<MenuItemDto> CreateAsync(MenuItemCreateDto input)
        {

            var menuItem = await _menuItemManager.CreateAsync(
            input.ParentId, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.Order, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName
            );

            return ObjectMapper.Map<MenuItem, MenuItemDto>(menuItem);
        }

        [Authorize(DynamicMenuPermissions.MenuItems.Edit)]
        public virtual async Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateDto input)
        {

            var menuItem = await _menuItemManager.UpdateAsync(
            id,
            input.ParentId, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.Order, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MenuItem, MenuItemDto>(menuItem);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MenuItemExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _menuItemRepository.GetListAsync(input.FilterText, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.OrderMin, input.OrderMax, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MenuItem>, List<MenuItemExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MenuItems.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MenuItemExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public virtual async Task<List<string>> GetPoliciesNamesAsync()
        {
            var dynamicPermissions = await _dynamicStore.GetPermissionsAsync();
            return dynamicPermissions.Select(p => p.Name).ToList();
        }

        public async Task<ListResultDto<MenuItemDto>> GetListAsync()
        {
            var menuItems = await _menuItemRepository.GetListAsync();

            return new ListResultDto<MenuItemDto>(
                    ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(menuItems)
            );
        }

        public async Task<PagedResultDto<MenuItemDto>> GetPageLookupAsync(GetMenuItemsInput input)
        {
            var totalCount = await _menuItemRepository.GetCountAsync(input.FilterText, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.OrderMin, input.OrderMax, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName, input.ParentId);
            var items = await _menuItemRepository.GetListAsync(input.FilterText, input.Name, input.DisplayName, input.IsActive, input.Url, input.Icon, input.OrderMin, input.OrderMax, input.Target, input.ElementId, input.CssClass, input.Permission, input.ResourceTypeName, input.ParentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MenuItemDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(items)
            };
        }
    }
}