using IBCL.Application.Common.Interfaces;
using IBCL.Application.Common.Models.Request.Positions;
using IBCL.Application.Common.Models.Response.Position;
using IBCL.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace IBCL.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost("savePosition")]
        public async Task SaveAsync([FromBody]SavePositionRequest request)
        {
            request.UserId = User.Identity.GetUserId();

            await _positionService.SavePositionAsync(request);
        }

        [HttpPut("updatePosition")]
        public async Task UpdateAsync([FromBody] UpdatePositionRequest request)
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
