using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public abstract class MenuItemsAppServiceTests<TStartupModule> : DynamicMenuApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IMenuItemsAppService _menuItemsAppService;
        private readonly IRepository<MenuItem, Guid> _menuItemRepository;

        public MenuItemsAppServiceTests()
        {
            _menuItemsAppService = GetRequiredService<IMenuItemsAppService>();
            _menuItemRepository = GetRequiredService<IRepository<MenuItem, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _menuItemsAppService.GetListAsync();

            // Assert
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8444d238-0865-46ec-b7f9-0f76142ac445")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _menuItemsAppService.GetAsync(Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new MenuItemCreateDto
            {
                Name = "6897d44c1acb4eefafadeeb664429c44217a5f8ebef640bb9a",
                DisplayName = "f3de4f04f16d40208e78d9b430742917b59dacecf69b4996ad",
                IsActive = true,
                Url = "d80e6dc7157a49efbe986ef5b4e43d8ad08188fdb62b4695aefb75fcb76ed5b687d67b4857324e68a64d8d638a1a89f8a",
                Icon = "396e8ae99cfd49b18a2d2114a62b6b89e95f42ef54be490190",
                Order = 713364479,
                Target = "b57109e2aa64440a",
                ElementId = "a720d2cfb16f425d98119b507465a6c70232373",
                CssClass = "bc584ad96f434e4ebdcec0",
                Permission = "03066044ec64455498dda6e59c2d96508d7a2ffb313440d7af7fa73eeb9c8c30912db540a5c545cd",
                ResourceTypeName = "74780ec1b70042258ab9de0f6d6f0ab985789e1d831d47079b6c90",
                ParentId = null
            };

            // Act
            var serviceResult = await _menuItemsAppService.CreateAsync(input);

            // Assert
            var result = await _menuItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("6897d44c1acb4eefafadeeb664429c44217a5f8ebef640bb9a");
            result.DisplayName.ShouldBe("f3de4f04f16d40208e78d9b430742917b59dacecf69b4996ad");
            result.IsActive.ShouldBe(true);
            result.Url.ShouldBe("d80e6dc7157a49efbe986ef5b4e43d8ad08188fdb62b4695aefb75fcb76ed5b687d67b4857324e68a64d8d638a1a89f8a");
            result.Icon.ShouldBe("396e8ae99cfd49b18a2d2114a62b6b89e95f42ef54be490190");
            result.Order.ShouldBe(713364479);
            result.Target.ShouldBe("b57109e2aa64440a");
            result.ElementId.ShouldBe("a720d2cfb16f425d98119b507465a6c70232373");
            result.CssClass.ShouldBe("bc584ad96f434e4ebdcec0");
            result.Permission.ShouldBe("03066044ec64455498dda6e59c2d96508d7a2ffb313440d7af7fa73eeb9c8c30912db540a5c545cd");
            result.ResourceTypeName.ShouldBe("74780ec1b70042258ab9de0f6d6f0ab985789e1d831d47079b6c90");
            result.ParentId.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new MenuItemUpdateDto()
            {
                Name = "2b5fca613726449bae98fbbca89d753406ca0d59d9e64be5b8",
                DisplayName = "a43ee8cf1b3548a0914b392f4601ba2ef6a8aa34b9fc463b9b",
                IsActive = true,
                Url = "a4d2c3dc656e4891a7123051e0df925e01be5e11748c4959a49f56d8fabadfd2f6e8aab987d341",
                Icon = "e2bb05a14c8b4023b90dee6c7e1582f08d43e9f98fb5436987",
                Order = 206199382,
                Target = "a52611ae91a144a8bdc66e82cadafaaa840e3c810499486bb76ff19ec37f3b9b4342369ab82f4d69af979421f308",
                ElementId = "d918e44f86ff46dd829d7c12c1797afb11e879c88dd24154bb46666e6d87fde5bd3969cb30ec4da2",
                CssClass = "7c7c7dc4e4fa46ac8c84594bbc7e6905b1d8a40d42cb43108f036e74e0caa530b38963704f684bfeb3ebcfae9d",
                Permission = "9371e388ba70465b85e015719314c983599034552df940168",
                ResourceTypeName = "75d0261ed9cd4e28bbb44f620a9e67952542eea43",
                ParentId = null
            };

            // Act
            var serviceResult = await _menuItemsAppService.UpdateAsync(Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"), input);

            // Assert
            var result = await _menuItemRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("2b5fca613726449bae98fbbca89d753406ca0d59d9e64be5b8");
            result.DisplayName.ShouldBe("a43ee8cf1b3548a0914b392f4601ba2ef6a8aa34b9fc463b9b");
            result.IsActive.ShouldBe(true);
            result.Url.ShouldBe("a4d2c3dc656e4891a7123051e0df925e01be5e11748c4959a49f56d8fabadfd2f6e8aab987d341");
            result.Icon.ShouldBe("e2bb05a14c8b4023b90dee6c7e1582f08d43e9f98fb5436987");
            result.Order.ShouldBe(206199382);
            result.Target.ShouldBe("a52611ae91a144a8bdc66e82cadafaaa840e3c810499486bb76ff19ec37f3b9b4342369ab82f4d69af979421f308");
            result.ElementId.ShouldBe("d918e44f86ff46dd829d7c12c1797afb11e879c88dd24154bb46666e6d87fde5bd3969cb30ec4da2");
            result.CssClass.ShouldBe("7c7c7dc4e4fa46ac8c84594bbc7e6905b1d8a40d42cb43108f036e74e0caa530b38963704f684bfeb3ebcfae9d");
            result.Permission.ShouldBe("9371e388ba70465b85e015719314c983599034552df940168");
            result.ResourceTypeName.ShouldBe("75d0261ed9cd4e28bbb44f620a9e67952542eea43");
            result.ParentId.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _menuItemsAppService.DeleteAsync(Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"));

            // Assert
            var result = await _menuItemRepository.FindAsync(c => c.Id == Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"));

            result.ShouldBeNull();
        }
    }
}