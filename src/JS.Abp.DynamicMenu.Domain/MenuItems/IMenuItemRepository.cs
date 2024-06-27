using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.DynamicMenu.MenuItems
{
    public interface IMenuItemRepository : IRepository<MenuItem, Guid>
    {
        Task<List<MenuItem>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    string? displayName = null,
                    bool? isActive = null,
                    string? url = null,
                    string? icon = null,
                    int? orderMin = null,
                    int? orderMax = null,
                    string? target = null,
                    string? elementId = null,
                    string? cssClass = null,
                    string? permission = null,
                    string? resourceTypeName = null,
                    Guid? parentId = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? displayName = null,
            bool? isActive = null,
            string? url = null,
            string? icon = null,
            int? orderMin = null,
            int? orderMax = null,
            string? target = null,
            string? elementId = null,
            string? cssClass = null,
            string? permission = null,
            string? resourceTypeName = null,
            Guid? parentId = null,
            CancellationToken cancellationToken = default);
    }
}