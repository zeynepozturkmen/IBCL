using Newtonsoft.Json;

namespace IBCL.Application.Common.Models.Request
{
    public class BaseRequestModel
    {
        [JsonIgnore]
        public Guid? UserId { get; set; }
    }
}
