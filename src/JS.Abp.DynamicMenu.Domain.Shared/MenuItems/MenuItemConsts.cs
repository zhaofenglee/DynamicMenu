namespace JS.Abp.DynamicMenu.MenuItems
{
    public static class MenuItemConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MenuItem." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int NameMaxLength = 50;
        public const int DisplayNameMinLength = 1;
        public const int DisplayNameMaxLength = 50;
        public const int IconMaxLength = 50;
        public const int ComponentMaxLength = 255;
    }
}