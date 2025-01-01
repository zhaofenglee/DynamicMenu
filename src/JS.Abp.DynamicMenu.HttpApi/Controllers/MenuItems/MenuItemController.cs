using JS.Abp.DynamicMenu.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using JS.Abp.DynamicMenu.MenuItems;
using Volo.Abp.Content;
using JS.Abp.DynamicMenu.Shared;

namespace JS.Abp.DynamicMenu.Controllers.MenuItems
{
    [RemoteService(Name = DynamicMenuRemoteServiceConsts.RemoteServiceName)]
    [Area("dynamicMenu")]
    [ControllerName("MenuItem")]
    [Route("api/dynamic-menu/menu-items")]

    public class MenuItemController : AbpController, IMenuItemsAppService
    {
        private readonly IMenuItemsAppService _menuItemsAppService;

        public MenuItemController(IMenuItemsAppService menuItemsAppService)
        {
            _menuItemsAppService = menuItemsAppService;
        }

        [HttpGet]
        public Task<ListResultDto<MenuItemDto>> GetListAsync()
        {
            return _menuItemsAppService.GetListAsync();
        }
        [HttpGet]
        [Route("lookup")]
        public Task<PagedResultDto<MenuItemDto>> GetPageLookupAsync(GetMenuItemsInput input)
        {
           return _menuItemsAppService.GetPageLookupAsync(input);
        }
        [HttpGet]
        [Route("tree")]
        public virtual Task<List<MenuItemTreeDto>> GetTreeAsync(string parentId = null)
        {
            return _menuItemsAppService.GetTreeAsync(parentId);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<MenuItemDto> GetAsync(Guid id)
        {
            return _menuItemsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("menu-item-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetMenuItemLookupAsync(LookupRequestDto input)
        {
            return _menuItemsAppService.GetMenuItemLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<MenuItemDto> CreateAsync(MenuItemCreateDto input)
        {
            return _menuItemsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateDto input)
        {
            return _menuItemsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _menuItemsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(MenuItemExcelDownloadDto input)
        {
            return _menuItemsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _menuItemsAppService.GetDownloadTokenAsync();
        }
        [HttpGet]
        [Route("policies-names")]
        public virtual Task<List<string>> GetPoliciesNamesAsync()
        {
            return _menuItemsAppService.GetPoliciesNamesAsync();
        }
        [HttpGet]
        [Route("current-user-menu-list")]
        public virtual Task<ListResultDto<MenuItemTreeDto>> GetCurrentUserMenuListAsync(GetCurrentUserMenuInput input)
        {
            return _menuItemsAppService.GetCurrentUserMenuListAsync(input);
        }
    }
}