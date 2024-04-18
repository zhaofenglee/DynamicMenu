using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Shared;

namespace JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems
{
    public class IndexModel : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public string? DisplayNameFilter { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public string? UrlFilter { get; set; }
        public string? IconFilter { get; set; }
        public int? OrderFilterMin { get; set; }

        public int? OrderFilterMax { get; set; }
        public string? TargetFilter { get; set; }
        public string? ElementIdFilter { get; set; }
        public string? CssClassFilter { get; set; }
        public string? PermissionFilter { get; set; }
        public string? ResourceTypeNameFilter { get; set; }
        public string? ParentIdFilter { get; set; }

        protected IMenuItemsAppService _menuItemsAppService;

        public IndexModel(IMenuItemsAppService menuItemsAppService)
        {
            _menuItemsAppService = menuItemsAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}