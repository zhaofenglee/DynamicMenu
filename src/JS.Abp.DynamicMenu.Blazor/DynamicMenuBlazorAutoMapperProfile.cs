using AutoMapper;
using JS.Abp.DynamicMenu.MenuItems;

namespace JS.Abp.DynamicMenu.Blazor;

public class DynamicMenuBlazorAutoMapperProfile : Profile
{
    public DynamicMenuBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<MenuItemDto, MenuItemUpdateDto>();
    }
}
