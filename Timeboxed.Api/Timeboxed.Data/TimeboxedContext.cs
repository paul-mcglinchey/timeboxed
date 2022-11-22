using Microsoft.EntityFrameworkCore;
using Timeboxed.Data.Converters;
using Timeboxed.Data.Extensions;
using Timeboxed.Domain.Models;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Data
{
    public class TimeboxedContext : DbContext
    {
        public TimeboxedContext()
        {
        }

        public TimeboxedContext(DbContextOptions<TimeboxedContext> options)
            : base(options) 
        {
        }

        public DbSet<Application>? Applications { get; set; }

        public DbSet<ApplicationPermission>? ApplicationPermissions { get; set; }

        public DbSet<Audit>? Audits { get; set; }

        public DbSet<Group>? Groups { get; set; }

        public DbSet<GroupApplication>? GroupApplications { get; set; }

        public DbSet<GroupUser>? GroupUsers { get; set; }

        public DbSet<GroupUserRole>? GroupUserRoles { get; set; }

        public DbSet<GroupUserApplication>? GroupUserApplications { get; set; }

        public DbSet<Permission>? Permissions { get; set; }

        public DbSet<Role>? Roles { get; set; }

        public DbSet<RolePermission>? RolePermissions { get; set; }

        public DbSet<User>? Users { get; set; }

        public DbSet<UserPreferences>? UserPreferences { get; set; }

        public DbSet<Client>? Clients { get; set; }

        public DbSet<Session>? Sessions { get; set; }

        public DbSet<Rota>? Rotas { get; set; }

        public DbSet<RotaEmployee>? RotaEmployees { get; set; }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Schedule>? Schedules { get; set; }

        public DbSet<EmployeeSchedule>? EmployeeSchedules { get; set; }

        public DbSet<EmployeeScheduleShift>? EmployeeScheduleShifts { get; set; }

        public DbSet<EmployeePreferences>? EmployeePreferences { get; set; }

        public DbSet<EmployeeUnavailableDays>? EmployeeUnavailableDays { get; set; }

        public DbSet<EmployeeHoliday>? EmployeeHolidays { get; set; }

        public DbSet<Tag>? Tags { get; set; }

        public DbSet<Email>? Emails { get; set; }

        public DbSet<PhoneNumber>? PhoneNumbers { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                                .HaveConversion<DateOnlyConverter>()
                                .HaveColumnType("date");

            configurationBuilder.Properties<Enums.DayOfWeek>()
                                .HaveConversion<DayOfWeekConverter>()
                                .HaveColumnType("smallint");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Extension method for seeding base Applications, Roles & Permissions
            modelBuilder.Seed();

            modelBuilder.Entity<Application>()
                .HasMany<Permission>(a => a.Permissions)
                .WithMany(p => p.Applications)
                .UsingEntity<ApplicationPermission>();

            modelBuilder.Entity<Group>()
                .HasMany<Application>(g => g.Applications)
                .WithMany(a => a.Groups)
                .UsingEntity<GroupApplication>();

            modelBuilder.Entity<Group>()
                .HasMany<GroupUser>(g => g.GroupUsers)
                .WithOne(gu => gu.Group);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User);

            modelBuilder.Entity<GroupUser>()
                .HasMany<Role>(gu => gu.Roles)
                .WithMany(r => r.GroupUsers)
                .UsingEntity<GroupUserRole>();

            modelBuilder.Entity<GroupUser>()
                .HasMany<Application>(gu => gu.Applications)
                .WithMany(a => a.GroupUsers)
                .UsingEntity<GroupUserApplication>();

            modelBuilder.Entity<Role>()
                .HasMany<Permission>(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>();

            modelBuilder.Entity<Rota>()
                .HasMany<RotaEmployee>(r => r.Employees)
                .WithOne(re => re.Rota);

            modelBuilder.Entity<RotaEmployee>()
                .HasOne(re => re.Rota)
                .WithMany(r => r.Employees)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<RotaEmployee>()
                .HasOne(re => re.Employee)
                .WithMany(e => e.Rotas)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
