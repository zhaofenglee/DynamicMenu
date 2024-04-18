using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using JS.Abp.DynamicMenu.MenuItems;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItemsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MenuItemsDataSeedContributor(IMenuItemRepository menuItemRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _menuItemRepository.InsertAsync(new MenuItem
            (
                id: Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388"),
                name: "7b5d1429d3a246b0863e582598c7dae89a7df3675f0b418484",
                displayName: "727c3e47edab49eaa5b4c874d06f3e095eff651ac301476c80",
                isActive: true,
                url: "49d7cd1112f0498982f9ca5bbc85a56c026c50d39b96448797e4a352ab6533de779edfb9b6d143a1bc73594a5",
                icon: "55e6d3d612154447935336f049f3ecb6496476139d3d45b0a2",
                order: 1383245460,
                target: "3425e93af9e84f1b839787",
                elementId: "59f1fd1b4c354e6ca347ecc1a874967caefc383f9ff64836b",
                cssClass: "29d17b27051542a7b966143c4f39b8214588b7cb1d2648c09774167c86e0403784a899af",
                permission: "d16ccea8e79",
                resourceTypeName: "4049e52ff3ce46eb908c8ffccc11085bd0d985c6d8ae49b594219d1",
                parentId: null
            ));

            await _menuItemRepository.InsertAsync(new MenuItem
            (
                id: Guid.Parse("8444d238-0865-46ec-b7f9-0f76142ac445"),
                name: "05a6aa1455494365b9e8ef32441c297e6c3813a93bdf41bbbd",
                displayName: "3b2b542705084dc4b62cce596988dde0a06d29bae6424c78b4",
                isActive: true,
                url: "b6681f7098564451b4f56953b0365950e46491efa877487e86b7646b4bef943111",
                icon: "f3c6692804cf4766a1dcec7f3e0b7ca382c41ca66dbe441f93",
                order: 858057849,
                target: "db9da10d3eae44a395fad37c324ebc",
                elementId: "5832fe8620234ba6b3331edccd54ee9b13c5cf",
                cssClass: "a756d315d4ef4853833806f443985e19868736054fdf46baa9e293deebd4fa9ed49c11911308",
                permission: "ecd62660b20c42b48",
                resourceTypeName: "a1c23041b41c4b27afe982a201770fb920416a5f42bc4278a",
                parentId: Guid.Parse("73275e9d-1989-47c9-9c9e-f62d093fc388")
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}