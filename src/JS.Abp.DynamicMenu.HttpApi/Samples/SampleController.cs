using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace JS.Abp.DynamicMenu.Samples;

[Area(DynamicMenuRemoteServiceConsts.ModuleName)]
[RemoteService(Name = DynamicMenuRemoteServiceConsts.RemoteServiceName)]
[Route("api/DynamicMenu/sample")]
public class SampleController : DynamicMenuController, ISampleAppService
{
    private readonly ISampleAppService _sampleAppService;

    public SampleController(ISampleAppService sampleAppService)
    {
        _sampleAppService = sampleAppService;
    }

    [HttpGet]
    public async Task<SampleDto> GetAsync()
    {
        return await _sampleAppService.GetAsync();
    }

    [HttpGet]
    [Route("authorized")]
    [Authorize]
    public async Task<SampleDto> GetAuthorizedAsync()
    {
        return await _sampleAppService.GetAsync();
    }
}
