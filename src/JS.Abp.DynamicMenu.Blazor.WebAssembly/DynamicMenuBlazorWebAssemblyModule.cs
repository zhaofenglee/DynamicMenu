﻿using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu.Blazor.WebAssembly;

[DependsOn(
    typeof(DynamicMenuBlazorModule),
    typeof(DynamicMenuHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class DynamicMenuBlazorWebAssemblyModule : AbpModule
{

}
