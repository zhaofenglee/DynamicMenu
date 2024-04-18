using JS.Abp.DynamicMenu.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.DynamicMenu.Shared;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public interface IMenuItemsAppService : IApplicationService
    {
        Task<ListResultDto<MenuItemDto>> GetListAsync();

        Task<PagedResultDto<MenuItemDto>> GetPageLookupAsync(GetMenuItemsInput input);
        
        Task<List<MenuItemTreeDto>> GetTreeAsync(string? parentId = null);

        Task<MenuItemDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetMenuItemLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<MenuItemDto> CreateAsync(MenuItemCreateDto input);

        Task<MenuItemDto> UpdateAsync(Guid id, MenuItemUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MenuItemExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();

        Task<List<string>> GetPoliciesNamesAsync();
    }
}