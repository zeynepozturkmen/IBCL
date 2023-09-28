using IBCL.Application.Common.Models.Response.Assets;

namespace IBCL.Application.Common.Interfaces
{
    public interface IAssetService
    {
        Task<List<AssetDto>> GetAllAsync();
    }
}
