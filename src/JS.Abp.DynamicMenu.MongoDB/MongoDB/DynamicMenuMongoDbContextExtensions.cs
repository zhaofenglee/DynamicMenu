using Volo.Abp;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicMenu.MongoDB;

public static class DynamicMenuMongoDbContextExtensions
{
    public static void ConfigureDynamicMenu(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
