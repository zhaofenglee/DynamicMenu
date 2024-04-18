using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class DynamicMenuDomainTestBase<TStartupModule> : DynamicMenuTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
