# JS.Abp.DynamicMenu

## Getting Started

### 1.Install the following NuGet packages.
  * JS.Abp.DynamicMenu.Application
  * JS.Abp.DynamicMenu.Application.Contracts
  * JS.Abp.DynamicMenu.Domain
  * JS.Abp.DynamicMenu.Domain.Shared
  * JS.Abp.DynamicMenu.EntityFrameworkCore
  * JS.Abp.DynamicMenu.HttpApi
  * JS.Abp.DynamicMenu.HttpApi.Client
  
### 2.Add `DependsOn` attribute to configure the module
 * [DependsOn(typeof(DynamicMenuApplicationModule))]
 * [DependsOn(typeof(DynamicMenuApplicationContractsModule))]
 * [DependsOn(typeof(DynamicMenuDomainModule))]
 * [DependsOn(typeof(DynamicMenuDomainSharedModule))]
 * [DependsOn(typeof(DynamicMenuEntityFrameworkCoreModule))]
 * [DependsOn(typeof(DynamicMenuHttpApiModule))]
 * [DependsOn(typeof(DynamicMenuHttpApiClientModule))]
 * [DependsOn(typeof(DynamicMenuBlazorServerModule))]



### 3. Add ` builder.ConfigureDynamicMenu();` to the `OnModelCreating()` method in **YourProjectDbContext.cs**.

### 4. Add EF Core migrations and update your database.
Open a command-line terminal in the directory of the YourProject.EntityFrameworkCore project and type the following command:

````bash
> dotnet ef migrations add Added_DynamicMenu
````
````bash
> dotnet ef database update
````

## Samples

See the [sample projects](https://github.com/zhaofenglee/JS.Abp.DynamicMenu/tree/master/host/JS.Abp.DynamicMenu.Blazor.Host)
