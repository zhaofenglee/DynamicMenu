using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItemTreeDto 
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public string? Target { get; set; }
        public string? ElementId { get; set; }
        public string? CssClass { get; set; }
        public string? Permission { get; set; }
        public string? ResourceTypeName { get; set; }
        public List<MenuItemTreeDto> Children { get; set; } = new List<MenuItemTreeDto>();

    }
}