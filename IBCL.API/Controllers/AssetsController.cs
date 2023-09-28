using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Response.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBCL.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        private readonly IAssetService _assetService;
        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

      
        [HttpGet("getAllAsset")]
        public async Task<ActionResult<List<AssetDto>>> GetAllAsync()
        {
            return Ok(await _assetService.GetAllAsync());
        }
    }
}
