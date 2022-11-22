using Microsoft.AspNetCore.Http;

namespace Timeboxed.Core.AccessControl.Interfaces
{
    public interface IGroupValidator
    {
        public Guid GroupId { get; }

        public Task Validate(CancellationToken cancellationToken = default);

        public Task Validate(HttpRequest req, CancellationToken cancellationToken = default);

        public Task Validate(string groupId, CancellationToken cancellationToken = default);
    }
}
