using JS.Abp.DynamicMenu.MenuItems;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu.Blazor;


[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemDtoToMenuItemUpdateDtoMapper : MapperBase<MenuItemDto, MenuItemUpdateDto>
{
    public override partial MenuItemUpdateDto Map(MenuItemDto source);
    
    public override partial void Map(MenuItemDto source, MenuItemUpdateDto destination);
}

