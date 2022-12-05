using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItemExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool? IsActive { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int? OrderMin { get; set; }
        public int? OrderMax { get; set; }
        public string Target { get; set; }
        public string ElementId { get; set; }
        public string CssClass { get; set; }
        public string Permission { get; set; }
        public string ResourceTypeName { get; set; }
        public Guid? ParentId { get; set; }

        public MenuItemExcelDownloadDto()
        {

        }
    }
}