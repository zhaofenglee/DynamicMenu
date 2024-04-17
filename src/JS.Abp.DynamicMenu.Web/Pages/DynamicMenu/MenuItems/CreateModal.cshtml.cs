using JS.Abp.DynamicMenu.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.DynamicMenu.MenuItems;

namespace JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems
{
    public class CreateModalModel : DynamicMenuPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ParentId { get; set; }
        
        [BindProperty]
        public MenuItemCreateViewModel MenuItem { get; set; }

        protected IMenuItemsAppService _menuItemsAppService;
        public List<SelectListItem> MenuItemsList { get; set; }

        public CreateModalModel(IMenuItemsAppService menuItemsAppService)
        {
            _menuItemsAppService = menuItemsAppService;
            MenuItemsList = new();
            MenuItem = new();
        }

        public virtual async Task OnGetAsync()
        {
            MenuItem = new MenuItemCreateViewModel();
            MenuItemsList.Add(new SelectListItem("",null));
            MenuItemsList.AddRange( (await _menuItemsAppService.GetListAsync())
                .Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList());
            MenuItem.ParentId = ParentId;
            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _menuItemsAppService.CreateAsync(ObjectMapper.Map<MenuItemCreateViewModel, MenuItemCreateDto>(MenuItem));
            return NoContent();
        }
    }

    public class MenuItemCreateViewModel : MenuItemCreateDto
    {
    }
}