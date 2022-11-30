using Timeboxed.Api.Models.Requests.Common;

namespace Timeboxed.Api.Models.Requests
{
    public class GetClientsRequest : PageableSortableRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
