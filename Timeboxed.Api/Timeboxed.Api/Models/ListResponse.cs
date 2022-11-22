using System.Collections.Generic;

namespace Timeboxed.Api.Models
{
    public class ListResponse<T>
    {
        public List<T> Items { get; set; } = new List<T>();

        public int Count { get; set; } = 0;
    }
}
