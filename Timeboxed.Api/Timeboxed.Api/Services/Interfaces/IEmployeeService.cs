using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ListResponse<EmployeeResponse>> GetEmployeesAsync(GetEmployeesRequest requestParameters, CancellationToken cancellationToken);

        public Task<EmployeeResponse> AddEmployeeAsync(AddEmployeeRequest request, CancellationToken cancellationToken);

        public Task<EmployeeResponse> UpdateEmployeeAsync(Guid employeeIdGuid, UpdateEmployeeRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteEmployeeAsync(Guid employeeIdGuid, CancellationToken cancellationToken);
    }
}
