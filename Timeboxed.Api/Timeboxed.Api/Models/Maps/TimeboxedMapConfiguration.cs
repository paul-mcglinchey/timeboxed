using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Models.DTOs.Common;
using Timeboxed.Domain.Models;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Api.Models.Maps
{
    public class TimeboxedMapConfiguration : Profile
    {
        public TimeboxedMapConfiguration()
        {
            this.CreateDTOMaps();
        }

        private async Task CreateDTOMaps()
        {
            this.CreateMap<User, UserDto>().ReverseMap();
            this.CreateMap<User, UserRequest>().ReverseMap();
            this.CreateMap<UserPreferences, UserPreferencesDto>().ReverseMap();
            this.CreateMap<Application, ApplicationDto>().ReverseMap();

            this.CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(p => p.Id)));

            this.CreateMap<Permission, PermissionDto>().ReverseMap();

            this.CreateMap<Rota, RotaDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees.Select(e => e.EmployeeId)))
                .ForMember(dest => dest.Schedules, opt => opt.MapFrom(src => src.Schedules.Select(s => s.Id)));
            this.CreateMap<RotaDto, Rota>()
                .ForMember(dest => dest.Employees, opt => opt.Ignore())
                .ForMember(dest => dest.Schedules, opt => opt.Ignore());

            this.CreateMap<Schedule, ScheduleDto>().ReverseMap();

            this.CreateMap<EmployeeSchedule, EmployeeScheduleDto>().ReverseMap();
            this.CreateMap<EmployeeScheduleShift, EmployeeScheduleShiftDto>().ReverseMap();

            this.CreateMap<Tracking, TrackingDto>().ReverseMap();
        }
    }
}
