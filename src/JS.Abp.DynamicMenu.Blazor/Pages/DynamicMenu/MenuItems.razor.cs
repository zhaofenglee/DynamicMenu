using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Permissions;
using JS.Abp.DynamicMenu.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.BlazoriseUI.Components;

namespace JS.Abp.DynamicMenu.Blazor.Pages.DynamicMenu
{
    public partial class MenuItems
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<MenuItemDto> MenuItemList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateMenuItem { get; set; }
        private bool CanEditMenuItem { get; set; }
        private bool CanDeleteMenuItem { get; set; }
        private MenuItemCreateDto NewMenuItem { get; set; }
        private Validations NewMenuItemValidations { get; set; }
        private MenuItemUpdateDto EditingMenuItem { get; set; }
        private Validations EditingMenuItemValidations { get; set; }
        private Guid EditingMenuItemId { get; set; }
        private Modal CreateMenuItemModal { get; set; }
        private Modal EditMenuItemModal { get; set; }
        private GetMenuItemsInput Filter { get; set; }
        private DataGridEntityActionsColumn<MenuItemDto> EntityActionsColumn { get; set; }
        protected string SelectedCreateTab = "menuItem-create-tab";
        protected string SelectedEditTab = "menuItem-edit-tab";
        private IReadOnlyList<LookupDto<Guid?>> MenuItemsNullable { get; set; } = new List<LookupDto<Guid?>>();
        private string MenuName { get; set; }
        private List<string> AbpPolicyNames  { get; set; } = new List<string>();
        List<MenuItemTreeDto> Items { get; set; } = new List<MenuItemTreeDto>();
        MenuItemTreeDto selectedNode = new MenuItemTreeDto();
        public MenuItems()
        {
            NewMenuItem = new MenuItemCreateDto();
            EditingMenuItem = new MenuItemUpdateDto();
            Filter = new GetMenuItemsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            await GetNullableMenuItemLookupAsync();
            await GetTreesAsync();

        }

        private async Task GetTreesAsync()
        {
            Items = await MenuItemsAppService.GetTreeAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:MenuItems"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewMenuItem"], async () =>
            {
                await OpenCreateMenuItemModalAsync();
            }, IconName.Add, requiredPolicyName: DynamicMenuPermissions.MenuItems.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateMenuItem = await AuthorizationService
                .IsGrantedAsync(DynamicMenuPermissions.MenuItems.Create);
            CanEditMenuItem = await AuthorizationService
                            .IsGrantedAsync(DynamicMenuPermissions.MenuItems.Edit);
            CanDeleteMenuItem = await AuthorizationService
                            .IsGrantedAsync(DynamicMenuPermissions.MenuItems.Delete);
        }

        private async Task GetMenuItemsAsync()
        {
            
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;
            Filter.ParentId = selectedNode.Id==Guid.Empty?null:selectedNode.Id;
               
            var result = await MenuItemsAppService.GetPageLookupAsync(Filter);
            MenuItemList = result.Items;
           
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetMenuItemsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await MenuItemsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync("Default");
            NavigationManager.NavigateTo($"{remoteService.BaseUrl.EnsureEndsWith('/')}api/dynamic-menu/menu-items/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<MenuItemDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetMenuItemsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateMenuItemModalAsync()
        {
            NewMenuItem = new MenuItemCreateDto{
                ParentId = selectedNode.Id==Guid.Empty?null:selectedNode.Id
                
            };
            
            await NewMenuItemValidations.ClearAll();
            await CreateMenuItemModal.Show();
        }

        private async Task CloseCreateMenuItemModalAsync()
        {
            NewMenuItem = new MenuItemCreateDto{
                
                
            };
            await CreateMenuItemModal.Hide();
        }

        private async Task OpenEditMenuItemModalAsync(MenuItemDto input)
        {
            var menuItem = await MenuItemsAppService.GetAsync(input.Id);
            
            EditingMenuItemId = menuItem.Id;
            EditingMenuItem = ObjectMapper.Map<MenuItemDto, MenuItemUpdateDto>(menuItem);
            await EditingMenuItemValidations.ClearAll();
            await EditMenuItemModal.Show();
        }

        private async Task DeleteMenuItemAsync(MenuItemDto input)
        {
            await MenuItemsAppService.DeleteAsync(input.Id);
            await GetMenuItemsAsync();
        }

        private async Task CreateMenuItemAsync()
        {
            try
            {
                if (await NewMenuItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await MenuItemsAppService.CreateAsync(NewMenuItem);
                await GetMenuItemsAsync();
                await GetTreesAsync();
                await CloseCreateMenuItemModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditMenuItemModalAsync()
        {
            await EditMenuItemModal.Hide();
        }

        private async Task UpdateMenuItemAsync()
        {
            try
            {
                if (await EditingMenuItemValidations.ValidateAll() == false)
                {
                    return;
                }

                await MenuItemsAppService.UpdateAsync(EditingMenuItemId, EditingMenuItem);
                await GetMenuItemsAsync();
                await GetTreesAsync();
                await EditMenuItemModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

        private async Task GetNullableMenuItemLookupAsync(string newValue = null)
        {
            AbpPolicyNames = (await MenuItemsAppService.GetPoliciesNamesAsync());
            MenuItemsNullable = (await MenuItemsAppService.GetMenuItemLookupAsync(new LookupRequestDto { Filter = newValue })).Items;
        }
        
        private async Task OnSelectedNodeChangedAsync()
        {
            await GetMenuItemsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }
}
