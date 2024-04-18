using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class DynamicMenuApplicationTestBase<TStartupModule> : DynamicMenuTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
