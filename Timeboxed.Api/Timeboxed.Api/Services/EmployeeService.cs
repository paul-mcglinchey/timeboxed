using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public EmployeeService(IMapper mapper, TimeboxedContext context, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.mapper = mapper;
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<EmployeeResponse>> GetEmployeesAsync(GetEmployeesRequest requestParameters, CancellationToken cancellationToken)
        {
            var employeeQuery = this.context.Employees.Where(e => this.groupContextProvider.GroupId == e.GroupId);

            employeeQuery = ApplyFilter(employeeQuery, requestParameters);
            employeeQuery = ApplySort(employeeQuery, requestParameters);

            var count = employeeQuery.Count();
            employeeQuery = employeeQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<EmployeeResponse>
            {
                Items = await employeeQuery.Select<Employee, EmployeeResponse>(e => e).ToListAsync(cancellationToken),
                Count = count,
            };
        }

        public async Task<EmployeeResponse> AddEmployeeAsync(AddEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                request.FirstName,
                request.LastName,
                request.PrimaryEmailAddress,
                this.groupContextProvider.GroupId,
                this.userContextProvider.UserId);

            this.context.Employees.Add(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return employee;
        }

        public async Task<EmployeeResponse> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees
                .Where(e => e.Id == employeeId && e.GroupId == this.groupContextProvider.GroupId)
                .Include(e => e.Holidays)
                .Include(e => e.UnavailableDays)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Employee {employeeId} not found");

            employee.FirstName = request.FirstName;
            employee.MiddleNames = request.MiddleNames;
            employee.LastName = request.LastName;
            employee.PrimaryEmailAddress = request.PrimaryEmailAddress;
            employee.PrimaryPhoneNumber = request.PrimaryPhoneNumber;
            employee.FirstLine = request.FirstLine;
            employee.SecondLine = request.SecondLine;
            employee.ThirdLine = request.ThirdLine;
            employee.City = request.City;
            employee.Country = request.Country;
            employee.PostCode = request.PostCode;
            employee.ZipCode = request.ZipCode;
            employee.BirthDate = request.BirthDate;
            employee.StartDate = request.StartDate;
            employee.ReportsToId = request.ReportsTo;
            employee.Role = request.Role;
            employee.MinHours = request.MinHours;
            employee.MaxHours = request.MaxHours;
            employee.Colour = request.Colour;

            employee.AddTracking(this.userContextProvider.UserId);

            await this.context.SaveChangesAsync(cancellationToken);

            return employee;
        }

        public async Task<Guid> DeleteEmployeeAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees
                .Where(e => e.Id == employeeId && e.GroupId == this.groupContextProvider.GroupId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Employee {employeeId} not found");

            this.context.Employees.Remove(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        private static IQueryable<Employee> ApplyFilter(IQueryable<Employee> query, GetEmployeesRequest request) => query;

        private IQueryable<Employee> ApplySort(IQueryable<Employee> query, GetEmployeesRequest request)
        {
            return request.SortField switch
            {
                _ => request.IsAscending
                    ? query.OrderBy(q => q.FirstName).ThenBy(q => q.LastName)
                    : query.OrderByDescending(q => q.FirstName).ThenBy(q => q.LastName)
            };
        }
    }
}
