using Microsoft.EntityFrameworkCore;
using Timeboxed.Data.Enums;
using Timeboxed.Domain.Models;

namespace Timeboxed.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var groupAdminRoleId = Guid.Parse("1BA0C5A2-00A5-4B37-9E97-CC354AD6D9E2");
            var groupMemberRoleId = Guid.Parse("39A759E0-4991-498F-8612-4E2FC709A9B2");
            var rotaManagerAdminRoleId = Guid.Parse("78114707-A61A-46E7-9914-F918DE3F89FA");
            var rotaManagerUserRoleId = Guid.Parse("24CDA806-3A28-45F8-9F6D-D64A613385CB");
            var clientManagerAdminRoleId = Guid.Parse("DCD622B3-400C-45E2-9451-23AF77B7F835");
            var clientManagerUserRoleId = Guid.Parse("16BEFBF9-1ADE-4FAD-B0ED-0080F82E8230");

            modelBuilder.Entity<Application>().HasData(
                new Application
                {
                    Id = 1,
                    Name = "Rota Manager",
                    Description = "A first class environment for managing rotas & employees in your business.",
                    Url = "/rotas/dashboard",
                    BackgroundImage = "https://res.cloudinary.com/pmcglinchey/image/upload/v1656632136/smoulderedsignals_19201080_m0nwwf.png",
                    BackgroundVideo = "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873263/smoulderingsignals_960540_looping_qu42se.mp4",
                    Colour = "#6d28d9",
                },
                new Application
                {
                    Id = 2,
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
                    Id = (int)TimeboxedPermission.GroupAccess,
                    Name = "Group Access",
                    Description = "Grants access to a group.",
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.GroupAdminAccess,
                    Name = "Group Admin Access",
                    Description = "Grants admin access to a group.",
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.ApplicationAccess,
                    Name = "Application Access",
                    Description = "Grants access to an application."
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.ApplicationAdminAccess,
                    Name = "Application Admin",
                    Description = "Grants admin access to an application.",
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.ViewRotas,
                    Name = "View Rotas",
                    Description = "Grants view access to rotas in the group.",
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.AddEditDeleteRotas,
                    Name = "Add, Edit & Delete Rotas",
                    Description = "Grants add, edit & delete access to rotas in a group.",
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.ViewClients,
                    Name = "View Clients",
                    Description = "Grants view access to clients in a group."
                },
                new Permission
                {
                    Id = (int)TimeboxedPermission.AddEditDeleteClients,
                    Name = "Add, Edit & Delete Clients",
                    Description = "Grants add, edit & delete access to clients in a group",
                });

            modelBuilder.Entity<ApplicationPermission>().HasData(
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.RotaManager,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.RotaManager,
                    PermissionId = (int)TimeboxedPermission.ApplicationAdminAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.RotaManager,
                    PermissionId = (int)TimeboxedPermission.ViewRotas,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.RotaManager,
                    PermissionId = (int)TimeboxedPermission.AddEditDeleteRotas,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.ClientManager,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.ClientManager,
                    PermissionId = (int)TimeboxedPermission.ApplicationAdminAccess,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.ClientManager,
                    PermissionId = (int)TimeboxedPermission.ViewClients,
                },
                new ApplicationPermission
                {
                    ApplicationId = (int)ApplicationType.ClientManager,
                    PermissionId = (int)TimeboxedPermission.AddEditDeleteClients,
                });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = groupAdminRoleId,
                    Name = "Group Admin",
                    Description = "Intended to be assigned to highest level group members."
                },
                new Role
                {
                    Id = groupMemberRoleId,
                    Name = "Group Member",
                    Description = "Intended to be assigned to base level group members."
                },
                new Role
                {
                    Id = rotaManagerUserRoleId,
                    Name = "Rota Manager User",
                    Description = "Intended to be assigned to base level rota manager users.",
                    ApplicationId = (int)ApplicationType.RotaManager,
                },
                new Role
                {
                    Id = rotaManagerAdminRoleId,
                    Name = "Rota Manager Admin",
                    Description = "Intended to be assigned to highest level rota manager users.",
                    ApplicationId = (int)ApplicationType.RotaManager,
                },
                new Role
                {
                    Id = clientManagerUserRoleId,
                    Name = "Client Manager User",
                    Description = "Intended to be assigned to base level client manager users.",
                    ApplicationId = (int)ApplicationType.ClientManager,
                },
                new Role
                {
                    Id = clientManagerAdminRoleId,
                    Name = "Client Manager Admin",
                    Description = "Intended to be assigned to highest level client manager users.",
                    ApplicationId = (int)ApplicationType.ClientManager,
                });

            modelBuilder.Entity<RolePermission>().HasData(
                // Group Admin Permissions
                new RolePermission
                {
                    RoleId = groupAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.GroupAccess
                }, 
                new RolePermission
                {
                    RoleId = groupAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.GroupAdminAccess
                },
                // Group Member Permissions
                new RolePermission
                {
                    RoleId = groupMemberRoleId,
                    PermissionId = (int)TimeboxedPermission.GroupAccess
                },
                // Rota Manager User Permissions
                new RolePermission
                {
                    RoleId = rotaManagerUserRoleId,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = rotaManagerUserRoleId,
                    PermissionId = (int)TimeboxedPermission.ViewRotas
                },
                // Rota Manager Admin Permissions
                new RolePermission
                {
                    RoleId = rotaManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = rotaManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.ViewRotas
                },
                new RolePermission
                {
                    RoleId = rotaManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.AddEditDeleteRotas
                },
                // Client Manager User Permissions
                new RolePermission
                {
                    RoleId = clientManagerUserRoleId,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = clientManagerUserRoleId,
                    PermissionId = (int)TimeboxedPermission.ViewClients
                },
                // Client Manager Admin Permissions
                new RolePermission
                {
                    RoleId = clientManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.ApplicationAccess
                },
                new RolePermission
                {
                    RoleId = clientManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.ViewClients
                },
                new RolePermission
                {
                    RoleId = clientManagerAdminRoleId,
                    PermissionId = (int)TimeboxedPermission.AddEditDeleteClients
                });
        }
    }
}
