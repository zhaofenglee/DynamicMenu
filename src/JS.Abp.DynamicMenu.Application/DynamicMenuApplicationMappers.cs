using System;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Shared;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemToMenuItemDtoMapper : MapperBase<MenuItem, MenuItemDto>
{
    public override partial MenuItemDto Map(MenuItem source);
    
    public override partial void Map(MenuItem source, MenuItemDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemToMenuItemExcelDtoMapper : MapperBase<MenuItem, MenuItemExcelDto>
{
    public override partial MenuItemExcelDto Map(MenuItem source);
    
    public override partial void Map(MenuItem source, MenuItemExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemToMenuItemTreeDtoMapper : MapperBase<MenuItem, MenuItemTreeDto>
{
    [MapperIgnoreTarget(nameof(MenuItemTreeDto.Children))]
    public override partial MenuItemTreeDto Map(MenuItem source);
    
    [MapperIgnoreTarget(nameof(MenuItemTreeDto.Children))]
    public override partial void Map(MenuItem source, MenuItemTreeDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemToLookupDtoMapper : MapperBase<MenuItem, LookupDto<Guid?>>
{
    [MapProperty(nameof(MenuItem.DisplayName), nameof(LookupDto<Guid?>.DisplayName))]
    public override partial LookupDto<Guid?> Map(MenuItem source);
    
    [MapProperty(nameof(MenuItem.DisplayName), nameof(LookupDto<Guid?>.DisplayName))]
    public override partial void Map(MenuItem source, LookupDto<Guid?> destination);
}

