using JS.Abp.DynamicMenu.Web.Pages.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.MenuItems;
using Riok.Mapperly.Abstractions;

namespace JS.Abp.DynamicMenu.Web;

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
