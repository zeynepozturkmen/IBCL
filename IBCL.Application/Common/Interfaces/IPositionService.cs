using IBCL.Application.Common.Models.Request.Positions;
using IBCL.Application.Common.Models.Response.Position;

namespace IBCL.Application.Common.Interfaces
{
    public interface IPositionService
    {
        Task SavePositionAsync(SavePositionRequest request);
        Task UpdatePositionAsync(UpdatePositionRequest request);
        Task<List<GetAllPositionResponse>> GetAllAsync(GetAllPositionRequest request);
    }
}
