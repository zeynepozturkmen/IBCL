using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Request.Positions;
using IBCL.Application.Common.Models.Response.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBCL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        public async Task SaveAsync(SavePositionRequest request)
        {
            await _positionService.SavePositionAsync(request);
        }

        [HttpPut("")]
        public async Task UpdateAsync(UpdatePositionRequest request)
        {
            await _positionService.UpdatePositionAsync(request);
        }

        [HttpGet("{accountId}/getAllpositions")]
        public async Task<ActionResult<List<GetAllPositionResponse>>> GetUserDocumentsAsync(Guid accountId)
        {
            return Ok(await _positionService.GetAllAsync(new GetAllPositionRequest() { AccountId = accountId }));
        }
    }
}
