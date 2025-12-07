using JS.Abp.DynamicMenu.MenuItems;
using Riok.Mapperly.Abstractions;

namespace JS.Abp.DynamicMenu.Blazor;

/// <summary>
/// Base class for one-way mapping from TSource to TDestination
/// </summary>
/// <typeparam name="TSource">Source type</typeparam>
/// <typeparam name="TDestination">Destination type</typeparam>
public abstract partial class MapperBase<TSource, TDestination>
{
    public abstract TDestination Map(TSource source);
    public abstract void Map(TSource source, TDestination destination);
    
    public virtual void AfterMap(TSource source, TDestination destination)
    {
    }
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MenuItemDtoToMenuItemUpdateDtoMapper : MapperBase<MenuItemDto, MenuItemUpdateDto>
{
    public override partial MenuItemUpdateDto Map(MenuItemDto source);
    
    public override partial void Map(MenuItemDto source, MenuItemUpdateDto destination);
}

