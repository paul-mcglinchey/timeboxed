using Microsoft.EntityFrameworkCore;
using Timeboxed.Data.Constants;
using Timeboxed.Domain.Models;

namespace Timeboxed.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>().HasData(
                new Application
                {
                    Id = TimeboxedApplications.RotaManager,
                    Name = "Rota Manager",
                    Description = "A first class environment for managing rotas & employees in your business.",
                    Url = "/rotas/dashboard",
                    BackgroundImage = "https://res.cloudinary.com/pmcglinchey/image/upload/v1656632136/smoulderedsignals_19201080_m0nwwf.png",
                    BackgroundVideo = "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873263/smoulderingsignals_960540_looping_qu42se.mp4",
                    Colour = "#6d28d9",
                },
                new Application
                {
                    Id = TimeboxedApplications.ClientManager,
                    Name = "Client Manager",
                    Description = "A complete package for managing clients allowing you to spend more time where it really matters.",
                    Url = "/clients/dashboard",
                    BackgroundImage = "https://res.cloudinary.com/pmcglinchey/image/upload/v1656621688/electricwaves_19201080_ll3sa9.png",
                    BackgroundVideo = "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873057/electricwaves_960540_looping_lczpjp.mp4",
                    Colour = "#e11d48",
                });

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    Id = TimeboxedPermissions.GroupAccess,
                    Name = "Group Access",
                    Description = "Grants access to a group.",
                },
                new Permission
                {
                    Id = TimeboxedPermissions.GroupAdminAccess,
                    Name = "Group Admin Access",
                    Description = "Grants admin access to a group.",
                },
                new Permission
                {
                    Id = TimeboxedPermissions.ApplicationAccess,
                    Name = "Application Access",
                    Description = "Grants access to an application."
                },
                new Permission
                {
                    Id = TimeboxedPermissions.ApplicationAdminAccess,
                    Name = "Application Admin",
                    Description = "Grants admin access to an application.",
                },
                new Permission
                {
                    Id = TimeboxedPermissions.ViewRotas,
                    Name = "View Rotas",
                    Description = "Grants view access to rotas in the group.",
                },
                new Permission
                {
                    Id = TimeboxedPermissions.AddEditDeleteRotas,
                    Name = "Add, Edit & Delete Rotas",
                    Description = "Grants add, edit & delete access to rotas in a group.",
                },
                new Permission
                {
                    Id = TimeboxedPermissions.ViewClients,
                    Name = "View Clients",
                    Description = "Grants view access to clients in a group."
                },
                new Permission
                {
                    Id = TimeboxedPermissions.AddEditDeleteClients,
                    Name = "Add, Edit & Delete Clients",
                    Description = "Grants add, edit & delete access to clients in a group",
                });

            modelBuilder.Entity<ApplicationPermission>().HasData(
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.RotaManager,
                    PermissionId = TimeboxedPermissions.ApplicationAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.RotaManager,
                    PermissionId = TimeboxedPermissions.ApplicationAdminAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.RotaManager,
                    PermissionId = TimeboxedPermissions.ViewRotas,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.RotaManager,
                    PermissionId = TimeboxedPermissions.AddEditDeleteRotas,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.ClientManager,
                    PermissionId = TimeboxedPermissions.ApplicationAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.ClientManager,
                    PermissionId = TimeboxedPermissions.ApplicationAdminAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.ClientManager,
                    PermissionId = TimeboxedPermissions.ViewClients,
                },
                new ApplicationPermission
                {
                    ApplicationId = TimeboxedApplications.ClientManager,
                    PermissionId = TimeboxedPermissions.AddEditDeleteClients,
                });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.GroupAdmin),
                    Name = "Group Admin",
                    Description = "Intended to be assigned to highest level group members."
                },
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.GroupMember),
                    Name = "Group Member",
                    Description = "Intended to be assigned to base level group members."
                },
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.RotaManagerUser),
                    Name = "Rota Manager User",
                    Description = "Intended to be assigned to base level rota manager users.",
                    ApplicationId = TimeboxedApplications.RotaManager,
                },
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.RotaManagerAdmin),
                    Name = "Rota Manager Admin",
                    Description = "Intended to be assigned to highest level rota manager users.",
                    ApplicationId = TimeboxedApplications.RotaManager,
                },
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.ClientManagerUser),
                    Name = "Client Manager User",
                    Description = "Intended to be assigned to base level client manager users.",
                    ApplicationId = TimeboxedApplications.ClientManager,
                },
                new Role
                {
                    Id = Guid.Parse(TimeboxedRoles.ClientManagerAdmin),
                    Name = "Client Manager Admin",
                    Description = "Intended to be assigned to highest level client manager users.",
                    ApplicationId = TimeboxedApplications.ClientManager,
                });

            modelBuilder.Entity<RolePermission>().HasData(
                // Group Admin Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.GroupAdmin),
                    PermissionId = TimeboxedPermissions.GroupAccess
                }, 
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.GroupAdmin),
                    PermissionId = TimeboxedPermissions.GroupAdminAccess
                },
                // Group Member Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.GroupMember),
                    PermissionId = TimeboxedPermissions.GroupAccess
                },
                // Rota Manager User Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.RotaManagerUser),
                    PermissionId = TimeboxedPermissions.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.RotaManagerUser),
                    PermissionId = TimeboxedPermissions.ViewRotas
                },
                // Rota Manager Admin Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.RotaManagerAdmin),
                    PermissionId = TimeboxedPermissions.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.RotaManagerAdmin),
                    PermissionId = TimeboxedPermissions.ViewRotas
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.RotaManagerAdmin),
                    PermissionId = TimeboxedPermissions.AddEditDeleteRotas
                },
                // Client Manager User Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.ClientManagerUser),
                    PermissionId = TimeboxedPermissions.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.ClientManagerUser),
                    PermissionId = TimeboxedPermissions.ViewClients
                },
                // Client Manager Admin Permissions
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.ClientManagerAdmin),
                    PermissionId = TimeboxedPermissions.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.ClientManagerAdmin),
                    PermissionId = TimeboxedPermissions.ViewClients
                },
                new RolePermission
                {
                    RoleId = Guid.Parse(TimeboxedRoles.ClientManagerAdmin),
                    PermissionId = TimeboxedPermissions.AddEditDeleteClients
                });
        }
    }
}
