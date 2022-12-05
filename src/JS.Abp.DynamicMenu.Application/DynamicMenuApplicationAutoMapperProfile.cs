using AutoMapper;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Shared;
using System;

namespace JS.Abp.DynamicMenu;

public class DynamicMenuApplicationAutoMapperProfile : Profile
{
    public DynamicMenuApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<MenuItem, MenuItemExcelDto>();

        CreateMap<MenuItemWithNavigationProperties, MenuItemWithNavigationPropertiesDto>();
        CreateMap<MenuItem, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName));
    }
}
