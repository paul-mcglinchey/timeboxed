using Timeboxed.Api.Models.Enums;

namespace Timeboxed.Api.Models
{
    public class PageableSortableRequest
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public string? SortField { get; set; }

        public SortDirection? SortDirection { get; set; }

        public bool IsAscending => (this.SortDirection ?? Enums.SortDirection.Descending) == Enums.SortDirection.Ascending;
    }
}
