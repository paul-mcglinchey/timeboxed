using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Core.Converters;
using Timeboxed.Domain.Models.Common;
using Timeboxed.Domain.Models;
using System.Linq;
using Newtonsoft.Json;

namespace Timeboxed.Api.Models.Responses
{
    public class EmployeeResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? Role { get; set; }

        public Guid? ReportsTo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleNames { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public string PrimaryPhoneNumber { get; set; }

        public List<EmailResponse> Emails { get; set; }

        public List<PhoneNumberResponse> PhoneNumbers { get; set; }

        public string? FirstLine { get; set; }

        public string? SecondLine { get; set; }

        public string? ThirdLine { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostCode { get; set; }

        public string? ZipCode { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? BirthDate { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        public List<EmployeeHolidayResponse> Holidays { get; set; }

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public List<DayOfWeek> UnavailableDays { get; set; }

        public string? Colour { get; set; }

        public bool? IsDeleted { get; set; }

        public static implicit operator EmployeeResponse(Employee employee) => new EmployeeResponse
        {
            Id = employee.Id,
            GroupId = employee.GroupId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            MiddleNames = employee.MiddleNames,
            PrimaryEmailAddress = employee.PrimaryEmailAddress,
            PrimaryPhoneNumber = employee.PrimaryPhoneNumber,
            Emails = employee.Emails.Select<Email, EmailResponse>(e => e).ToList(),
            PhoneNumbers = employee.PhoneNumbers.Select<PhoneNumber, PhoneNumberResponse>(pn => pn).ToList(),
            FirstLine = employee.FirstLine,
            SecondLine = employee.SecondLine,
            ThirdLine = employee.ThirdLine,
            City = employee.City,
            Country = employee.Country,
            PostCode = employee.PostCode,
            ZipCode = employee.ZipCode,
            BirthDate = employee.BirthDate,
            StartDate = employee.StartDate,
            Holidays = employee.Holidays.Select<EmployeeHoliday, EmployeeHolidayResponse>(eh => eh).ToList(),
            Colour = employee.Colour,
            MinHours = employee.MinHours,
            MaxHours = employee.MaxHours,
            UnavailableDays = employee.UnavailableDays.Select(ud => ud.DayOfWeek).ToList(),
            Role = employee.Role,
            ReportsTo = employee.ReportsToId,
            UpdatedAt = employee.UpdatedAt,
            CreatedAt = employee.CreatedAt,
            UpdatedBy = employee.UpdatedBy,
            CreatedBy = employee.CreatedBy,
        };
    }

    public class EmployeeHolidayResponse
    {
        public Guid? Id { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? EndDate { get; set; }

        public bool? IsPaid { get; set; }

        public string? Notes { get; set; }

        public static implicit operator EmployeeHolidayResponse(EmployeeHoliday eh) => new EmployeeHolidayResponse
        {
            Id = eh.Id,
            StartDate = eh.StartDate,
            EndDate = eh.EndDate,
            IsPaid = eh.IsPaid,
            Notes = eh.Notes,
        };
    }
}
