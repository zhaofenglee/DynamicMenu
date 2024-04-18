using JS.Abp.DynamicMenu.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.DynamicMenu.MenuItems;

namespace JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems
{
    public class EditModalModel : DynamicMenuPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public MenuItemUpdateViewModel MenuItem { get; set; }

        protected IMenuItemsAppService _menuItemsAppService;
        public List<SelectListItem> MenuItemsList { get; set; }
        
        public List<SelectListItem> AbpPolicyNames { get; set; }
        public EditModalModel(IMenuItemsAppService menuItemsAppService)
        {
            _menuItemsAppService = menuItemsAppService;
            MenuItemsList = new();
            MenuItem = new();
            AbpPolicyNames = new();
        }

        public virtual async Task OnGetAsync()
        {
            MenuItemsList.Add(new SelectListItem("",null));
            MenuItemsList.AddRange( (await _menuItemsAppService.GetListAsync())
                .Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList());
            AbpPolicyNames.Add(new SelectListItem("",null));
            AbpPolicyNames.AddRange((await _menuItemsAppService.GetPoliciesNamesAsync()).Select(x=> new SelectListItem(x,x)).ToList());
            var menuItem = await _menuItemsAppService.GetAsync(Id);
            MenuItem = ObjectMapper.Map<MenuItemDto, MenuItemUpdateViewModel>(menuItem);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _menuItemsAppService.UpdateAsync(Id, ObjectMapper.Map<MenuItemUpdateViewModel, MenuItemUpdateDto>(MenuItem));
            return NoContent();
        }
    }

    public class MenuItemUpdateViewModel : MenuItemUpdateDto
    {
    }
}