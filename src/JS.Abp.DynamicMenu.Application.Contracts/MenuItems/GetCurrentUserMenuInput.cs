using System.ComponentModel.DataAnnotations;

namespace JS.Abp.DynamicMenu.MenuItems;

public class GetCurrentUserMenuInput
{
    [Required]
    public string? Name { get; set; } = null!;
}