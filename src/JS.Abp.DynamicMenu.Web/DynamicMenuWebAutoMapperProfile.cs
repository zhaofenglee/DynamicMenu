using JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems;
using Volo.Abp.AutoMapper;
using JS.Abp.DynamicMenu.MenuItems;
using AutoMapper;

namespace JS.Abp.DynamicMenu.Web;

public class DynamicMenuWebAutoMapperProfile : Profile
{
    public DynamicMenuWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<MenuItemDto, MenuItemUpdateViewModel>();
        CreateMap<MenuItemUpdateViewModel, MenuItemUpdateDto>();
        CreateMap<MenuItemCreateViewModel, MenuItemCreateDto>();
    }
}