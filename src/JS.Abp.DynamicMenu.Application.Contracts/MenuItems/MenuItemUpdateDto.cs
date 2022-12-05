using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItemUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(MenuItemConsts.NameMaxLength, MinimumLength = MenuItemConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(MenuItemConsts.DisplayNameMaxLength, MinimumLength = MenuItemConsts.DisplayNameMinLength)]
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }
        [StringLength(MenuItemConsts.IconMaxLength)]
        public string Icon { get; set; }
        public int Order { get; set; }
        public string Target { get; set; }
        public string ElementId { get; set; }
        public string CssClass { get; set; }
        public string Permission { get; set; }
        public string ResourceTypeName { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}