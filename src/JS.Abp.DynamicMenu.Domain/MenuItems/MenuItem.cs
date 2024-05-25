using JS.Abp.DynamicMenu.MenuItems;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public class MenuItem : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string DisplayName { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string? Url { get; set; }

        public virtual string? Icon { get; set; }

        public virtual int Order { get; set; }

        public virtual string? Target { get; set; }

        public virtual string? ElementId { get; set; }

        public virtual string? CssClass { get; set; }

        public virtual string? Permission { get; set; }

        public virtual string? ResourceTypeName { get; set; }
        public Guid? ParentId { get; set; }

        public MenuItem()
        {

        }

        public MenuItem(Guid id, Guid? parentId, string name, string displayName, bool isActive, string url, string icon, int order, string target, string elementId, string cssClass, string permission, string resourceTypeName)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), MenuItemConsts.NameMaxLength, MenuItemConsts.NameMinLength);
            Check.NotNull(displayName, nameof(displayName));
            Check.Length(displayName, nameof(displayName), MenuItemConsts.DisplayNameMaxLength, MenuItemConsts.DisplayNameMinLength);
            Check.Length(icon, nameof(icon), MenuItemConsts.IconMaxLength, 0);
            Name = name;
            DisplayName = displayName;
            IsActive = isActive;
            Url = url;
            Icon = icon;
            Order = order;
            Target = target;
            ElementId = elementId;
            CssClass = cssClass;
            Permission = permission;
            ResourceTypeName = resourceTypeName;
            ParentId = parentId;
        }

    }
}