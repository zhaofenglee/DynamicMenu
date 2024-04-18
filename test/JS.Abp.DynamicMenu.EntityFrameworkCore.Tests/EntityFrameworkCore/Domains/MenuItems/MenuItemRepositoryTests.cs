using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.DynamicMenu.EntityFrameworkCore.Domains.MenuItems
{
    public class MenuItemRepositoryTests : DynamicMenuEntityFrameworkCoreTestBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemRepositoryTests()
        {
            _menuItemRepository = GetRequiredService<IMenuItemRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _menuItemRepository.GetListAsync(
                    name: "05a6aa1455494365b9e8ef32441c297e6c3813a93bdf41bbbd",
                    displayName: "3b2b542705084dc4b62cce596988dde0a06d29bae6424c78b4",
                    isActive: true,
                    url: "b6681f7098564451b4f56953b0365950e46491efa877487e86b7646b4bef943111",
                    icon: "f3c6692804cf4766a1dcec7f3e0b7ca382c41ca66dbe441f93",
                    target: "db9da10d3eae44a395fad37c324ebc",
                    elementId: "5832fe8620234ba6b3331edccd54ee9b13c5cf",
                    cssClass: "a756d315d4ef4853833806f443985e19868736054fdf46baa9e293deebd4fa9ed49c11911308",
                    permission: "ecd62660b20c42b48",
                    resourceTypeName: "a1c23041b41c4b27afe982a201770fb920416a5f42bc4278a",
                    parentId: Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8444d238-0865-46ec-b7f9-0f76142ac445"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _menuItemRepository.GetCountAsync(
                    name: "05a6aa1455494365b9e8ef32441c297e6c3813a93bdf41bbbd",
                    displayName: "3b2b542705084dc4b62cce596988dde0a06d29bae6424c78b4",
                    isActive: true,
                    url: "b6681f7098564451b4f56953b0365950e46491efa877487e86b7646b4bef943111",
                    icon: "f3c6692804cf4766a1dcec7f3e0b7ca382c41ca66dbe441f93",
                    target: "db9da10d3eae44a395fad37c324ebc",
                    elementId: "5832fe8620234ba6b3331edccd54ee9b13c5cf",
                    cssClass: "a756d315d4ef4853833806f443985e19868736054fdf46baa9e293deebd4fa9ed49c11911308",
                    permission: "ecd62660b20c42b48",
                    resourceTypeName: "a1c23041b41c4b27afe982a201770fb920416a5f42bc4278a",
                    parentId: Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}