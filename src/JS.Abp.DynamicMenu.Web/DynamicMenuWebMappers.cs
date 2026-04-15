using JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.MenuItems;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu.Web;


[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemDtoToMenuItemUpdateViewModelMapper : MapperBase<MenuItemDto, MenuItemUpdateViewModel>
{
    public override partial MenuItemUpdateViewModel Map(MenuItemDto source);

    public override partial void Map(MenuItemDto source, MenuItemUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemUpdateViewModelToMenuItemUpdateDtoMapper : MapperBase<MenuItemUpdateViewModel, MenuItemUpdateDto>
{
    public override partial MenuItemUpdateDto Map(MenuItemUpdateViewModel source);

    public override partial void Map(MenuItemUpdateViewModel source, MenuItemUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemCreateViewModelToMenuItemCreateDtoMapper : MapperBase<MenuItemCreateViewModel, MenuItemCreateDto>
{
    public override partial MenuItemCreateDto Map(MenuItemCreateViewModel source);

    public override partial void Map(MenuItemCreateViewModel source, MenuItemCreateDto destination);
}
