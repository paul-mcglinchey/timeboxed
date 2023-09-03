using Microsoft.AspNetCore.Mvc;

namespace Timeboxed.Core.Controllers.ActionResults
{
    public class BadRequest : BadRequestObjectResult
    {
        public BadRequest(string message)
            :base(new { Id = Guid.NewGuid(), Message = message })
        {
        }
    }
}
