﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<UserAccessControl>? UserAccessControl { get; set; }

        public DbSet<Client>? Clients { get; set; }

        public DbSet<Session>? Sessions { get; set; }

        public DbSet<SessionTag>? SessionTags { get; set; }

        public DbSet<GroupClientTag>? GroupClientTags { get; set; }

        public DbSet<Rota>? Rotas { get; set; }

        public DbSet<RotaEmployee>? RotaEmployees { get; set; }

        public DbSet<Employee>? Employees { get; set; }

        public DbSet<Schedule>? Schedules { get; set; }

        public DbSet<EmployeeSchedule>? EmployeeSchedules { get; set; }

        public DbSet<EmployeeScheduleShift>? EmployeeScheduleShifts { get; set; }

        public DbSet<EmployeeUnavailableDays>? EmployeeUnavailableDays { get; set; }

        public DbSet<EmployeeHoliday>? EmployeeHolidays { get; set; }

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

            // Groups
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

            modelBuilder.Entity<Session>()
                .HasMany<SessionTag>(s => s.Tags)
                .WithOne(st => st.Session);

            modelBuilder.Entity<SessionTag>()
                .HasOne<GroupClientTag>(st => st.GroupClientTag)
                .WithMany(gct => gct.Tags)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GroupClientTag>()
                .HasOne<Group>(gct => gct.Group)
                .WithMany();

            // Rotas
            modelBuilder.Entity<Rota>().HasMany<Employee>(r => r.Employees).WithMany(e => e.Rotas).UsingEntity<RotaEmployee>().HasKey(re => new { re.RotaId, re.EmployeeId });
            modelBuilder.Entity<RotaEmployee>().HasOne<Rota>(re => re.Rota).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<RotaEmployee>().HasOne<Employee>(re => re.Employee).WithMany().OnDelete(DeleteBehavior.NoAction);

            // Meta Info
            modelBuilder.Entity<Role>()
                .HasMany<Permission>(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>();

            modelBuilder.Entity<Application>()
                .HasMany<Permission>(a => a.Permissions)
                .WithMany(p => p.Applications)
                .UsingEntity<ApplicationPermission>();
        }
    }
}
