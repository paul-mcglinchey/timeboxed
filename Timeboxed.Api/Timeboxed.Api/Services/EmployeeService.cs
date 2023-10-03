using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Api.Services.Projections;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public EmployeeService(TimeboxedContext context, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<EmployeeListResponse>> GetEmployeesAsync(GetEmployeesRequest requestParameters, CancellationToken cancellationToken)
        {
            var employeeQuery = this.context.Employees.Where(e => this.groupContextProvider.GroupId == e.GroupId);

            employeeQuery = ApplyFilter(employeeQuery, requestParameters);
            employeeQuery = ApplySort(employeeQuery, requestParameters);

            var count = employeeQuery.Count();
            employeeQuery = employeeQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<EmployeeListResponse>
            {
                Items = await employeeQuery.Select(MapEFEmployeeToListResponse).ToListAsync(cancellationToken),
                Count = count,
            };
        }

        public async Task<EmployeeResponse> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken) =>
            await this.context.Employees
                .Where(e => e.Id == employeeId && e.IsDeleted != true)
                .Select(MapEFEmployeeToResponse)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Employee {employeeId} not found");

        public async Task<Guid> AddEmployeeAsync(AddEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                request.FirstName,
                request.LastName,
                request.PrimaryEmailAddress,
                this.groupContextProvider.GroupId,
                this.userContextProvider.UserId);

            this.context.Employees.Add(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        public async Task UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request, CancellationToken cancellationToken)
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
        }

        public async Task<Guid> DeleteEmployeeAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees
                .Where(e => e.Id == employeeId && e.GroupId == this.groupContextProvider.GroupId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Employee {employeeId} not found");

            employee.IsDeleted = true;
            await this.context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        private static IQueryable<Employee> ApplyFilter(IQueryable<Employee> query, GetEmployeesRequest request) => query.Where(e => e.IsDeleted != true);

        private IQueryable<Employee> ApplySort(IQueryable<Employee> query, GetEmployeesRequest request)
        {
            return request.SortField switch
            {
                _ => request.IsAscending
                    ? query.OrderBy(q => q.FirstName).ThenBy(q => q.LastName)
                    : query.OrderByDescending(q => q.FirstName).ThenBy(q => q.LastName)
            };
        }

        private static Expression<Func<Employee, EmployeeListResponse>> MapEFEmployeeToListResponse => (Employee e) => new EmployeeListResponse
        {
            Id = e.Id,
            Role = e.Role,
            GroupId = e.GroupId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            MiddleNames = e.MiddleNames,
            PrimaryEmailAddress = e.PrimaryEmailAddress,
            Holidays = e.Holidays.Select(h => new EmployeeHolidayResponse
            {
                Id = h.Id,
                StartDate = h.StartDate,
                EndDate = h.EndDate,
                Notes = h.Notes,
                IsPaid = h.IsPaid
            }).ToList(),
            MinHours = e.MinHours,
            MaxHours = e.MaxHours,
            UnavailableDays = e.UnavailableDays.Select(ud => (DayOfWeek)ud.DayOfWeek).ToList(),
            Colour = e.Colour,
            IsDeleted = e.IsDeleted,
            UpdatedAt = e.UpdatedAt,
            UpdatedBy = e.UpdatedBy,
            CreatedAt = e.CreatedAt,
            CreatedBy = e.CreatedBy,
        };

        private static Expression<Func<Employee, EmployeeResponse>> MapEFEmployeeToResponse => (Employee e) => new EmployeeResponse
        {
            Id = e.Id,
            Role = e.Role,
            GroupId = e.GroupId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            MiddleNames = e.MiddleNames,
            PrimaryEmailAddress = e.PrimaryEmailAddress,
            PrimaryPhoneNumber = e.PrimaryPhoneNumber,
            Emails = e.Emails.AsQueryable().Select(Common.MapEFEmailToResponse).ToList(),
            PhoneNumbers = e.PhoneNumbers.AsQueryable().Select(Common.MapEFPhoneNumberToResponse).ToList(),
            FirstLine = e.FirstLine,
            SecondLine = e.SecondLine,
            ThirdLine = e.ThirdLine,
            City = e.City,
            Country = e.Country,
            PostCode = e.PostCode,
            ZipCode = e.ZipCode,
            BirthDate = e.BirthDate,
            StartDate = e.StartDate,
            Holidays = e.Holidays.Select(h => new EmployeeHolidayResponse
            {
                Id = h.Id,
                StartDate = h.StartDate,
                EndDate = h.EndDate,
                Notes = h.Notes,
                IsPaid = h.IsPaid
            }).ToList(),
            MinHours = e.MinHours,
            MaxHours = e.MaxHours,
            UnavailableDays = e.UnavailableDays.Select(ud => (DayOfWeek)ud.DayOfWeek).ToList(),
            ReportsTo = e.ReportsTo.Id,
            Colour = e.Colour,
            IsDeleted = e.IsDeleted,
            UpdatedAt = e.UpdatedAt,
            UpdatedBy = e.UpdatedBy,
            CreatedAt = e.CreatedAt,
            CreatedBy = e.CreatedBy,
        };
    }
}
