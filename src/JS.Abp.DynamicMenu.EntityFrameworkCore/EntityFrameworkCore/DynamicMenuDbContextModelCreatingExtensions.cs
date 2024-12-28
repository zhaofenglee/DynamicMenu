using JS.Abp.DynamicMenu.MenuItems;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace JS.Abp.DynamicMenu.EntityFrameworkCore;

public static class DynamicMenuDbContextModelCreatingExtensions
{
    public static void ConfigureDynamicMenu(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        if (builder.IsHostDatabase())
        {
            builder.Entity<MenuItem>(b =>
            {
                b.ToTable(DynamicMenuDbProperties.DbTablePrefix + "MenuItems", DynamicMenuDbProperties.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(MenuItem.Name)).IsRequired().HasMaxLength(MenuItemConsts.NameMaxLength);
                b.Property(x => x.DisplayName).HasColumnName(nameof(MenuItem.DisplayName)).IsRequired().HasMaxLength(MenuItemConsts.DisplayNameMaxLength);
                b.Property(x => x.IsActive).HasColumnName(nameof(MenuItem.IsActive));
                b.Property(x => x.Url).HasColumnName(nameof(MenuItem.Url));
                b.Property(x => x.Icon).HasColumnName(nameof(MenuItem.Icon)).HasMaxLength(MenuItemConsts.IconMaxLength);
                b.Property(x => x.Order).HasColumnName(nameof(MenuItem.Order));
                b.Property(x => x.Target).HasColumnName(nameof(MenuItem.Target));
                b.Property(x => x.ElementId).HasColumnName(nameof(MenuItem.ElementId));
                b.Property(x => x.CssClass).HasColumnName(nameof(MenuItem.CssClass));
                b.Property(x => x.Permission).HasColumnName(nameof(MenuItem.Permission));
                b.Property(x => x.ResourceTypeName).HasColumnName(nameof(MenuItem.ResourceTypeName));
                b.Property(x=>x.Component).HasColumnName(nameof(MenuItem.Component)).HasMaxLength(MenuItemConsts.ComponentMaxLength);
                b.HasOne<MenuItem>().WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(DynamicMenuDbProperties.DbTablePrefix + "Questions", DynamicMenuDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
