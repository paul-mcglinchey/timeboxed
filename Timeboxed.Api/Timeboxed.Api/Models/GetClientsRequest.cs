namespace Timeboxed.Api.Models
{
    public class GetClientsRequest : PageableSortableRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }
    }
}
