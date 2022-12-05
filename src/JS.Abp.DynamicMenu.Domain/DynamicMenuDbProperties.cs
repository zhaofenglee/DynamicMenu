namespace JS.Abp.DynamicMenu;

public static class DynamicMenuDbProperties
{
    public static string DbTablePrefix { get; set; } = "Abp";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DynamicMenu";
}
