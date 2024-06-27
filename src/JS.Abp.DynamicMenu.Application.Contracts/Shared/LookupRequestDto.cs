using Volo.Abp.Application.Dtos;

namespace JS.Abp.DynamicMenu.Shared
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public LookupRequestDto()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
}