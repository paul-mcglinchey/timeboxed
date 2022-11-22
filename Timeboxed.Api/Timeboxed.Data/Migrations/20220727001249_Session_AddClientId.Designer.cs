﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timeboxed.Data;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    [DbContext(typeof(TimeboxedContext))]
    [Migration("20220727001249_Session_AddClientId")]
    partial class Session_AddClientId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tiebreaker.Domain.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AuditId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BackgroundImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Applications");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            BackgroundImage = "https://res.cloudinary.com/pmcglinchey/image/upload/v1656632136/smoulderedsignals_19201080_m0nwwf.png",
                            BackgroundVideo = "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873263/smoulderingsignals_960540_looping_qu42se.mp4",
                            Colour = "#6d28d9",
                            Description = "A first class environment for managing rotas & employees in your business.",
                            Name = "Rota Manager",
                            Url = "/rotas/dashboard"
                        },
                        new
                        {
                            Id = 2,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            BackgroundImage = "https://res.cloudinary.com/pmcglinchey/image/upload/v1656621688/electricwaves_19201080_ll3sa9.png",
                            BackgroundVideo = "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873057/electricwaves_960540_looping_lczpjp.mp4",
                            Colour = "#e11d48",
                            Description = "A complete package for managing clients allowing you to spend more time where it really matters.",
                            Name = "Client Manager",
                            Url = "/clients/dashboard"
                        });
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.ApplicationPermission", b =>
                {
                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("ApplicationId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("ApplicationPermissions");

                    b.HasData(
                        new
                        {
                            ApplicationId = 1,
                            PermissionId = 101
                        },
                        new
                        {
                            ApplicationId = 1,
                            PermissionId = 102
                        },
                        new
                        {
                            ApplicationId = 1,
                            PermissionId = 201
                        },
                        new
                        {
                            ApplicationId = 1,
                            PermissionId = 202
                        },
                        new
                        {
                            ApplicationId = 2,
                            PermissionId = 101
                        },
                        new
                        {
                            ApplicationId = 2,
                            PermissionId = 102
                        },
                        new
                        {
                            ApplicationId = 2,
                            PermissionId = 301
                        },
                        new
                        {
                            ApplicationId = 2,
                            PermissionId = 302
                        });
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContactInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactInfoId");

                    b.HasIndex("GroupId");

                    b.HasIndex("NameId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThirdLine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.ContactInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PrimaryEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactInfos");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ContactInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.Name", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleNames")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Names");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.PhoneNumber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupApplication", b =>
                {
                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupApplications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("HasJoined")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<Guid>("GroupUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("GroupUserId");

                    b.ToTable("GroupUserApplications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserApplicationRole", b =>
                {
                    b.Property<Guid>("GroupUserApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupUserApplicationId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("GroupUserApplicationRoles");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserRole", b =>
                {
                    b.Property<Guid>("GroupUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupUserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("GroupUserRoles");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AuditId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants access to a group.",
                            Language = "en-US",
                            Name = "Group Access"
                        },
                        new
                        {
                            Id = 2,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants admin access to a group.",
                            Language = "en-US",
                            Name = "Group Admin Access"
                        },
                        new
                        {
                            Id = 101,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants access to an application.",
                            Language = "en-US",
                            Name = "Application Access"
                        },
                        new
                        {
                            Id = 102,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants admin access to an application.",
                            Language = "en-US",
                            Name = "Application Admin"
                        },
                        new
                        {
                            Id = 201,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants view access to rotas in the group.",
                            Language = "en-US",
                            Name = "View Rotas"
                        },
                        new
                        {
                            Id = 202,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants add, edit & delete access to rotas in a group.",
                            Language = "en-US",
                            Name = "Add, Edit & Delete Rotas"
                        },
                        new
                        {
                            Id = 301,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants view access to clients in a group.",
                            Language = "en-US",
                            Name = "View Clients"
                        },
                        new
                        {
                            Id = 302,
                            AuditId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "Grants add, edit & delete access to clients in a group",
                            Language = "en-US",
                            Name = "Add, Edit & Delete Clients"
                        });
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("GroupId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2"),
                            Description = "Intended to be assigned to highest level group members.",
                            Name = "Group Admin"
                        },
                        new
                        {
                            Id = new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2"),
                            Description = "Intended to be assigned to base level group members.",
                            Name = "Group Member"
                        },
                        new
                        {
                            Id = new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb"),
                            ApplicationId = 1,
                            Description = "Intended to be assigned to base level rota manager users.",
                            Name = "Rota Manager User"
                        },
                        new
                        {
                            Id = new Guid("78114707-a61a-46e7-9914-f918de3f89fa"),
                            ApplicationId = 1,
                            Description = "Intended to be assigned to highest level rota manager users.",
                            Name = "Rota Manager Admin"
                        },
                        new
                        {
                            Id = new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230"),
                            ApplicationId = 2,
                            Description = "Intended to be assigned to base level client manager users.",
                            Name = "Client Manager User"
                        },
                        new
                        {
                            Id = new Guid("dcd622b3-400c-45e2-9451-23af77b7f835"),
                            ApplicationId = 2,
                            Description = "Intended to be assigned to highest level client manager users.",
                            Name = "Client Manager Admin"
                        });
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.RolePermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            RoleId = new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2")
                        },
                        new
                        {
                            PermissionId = 2,
                            RoleId = new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2")
                        },
                        new
                        {
                            PermissionId = 1,
                            RoleId = new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2")
                        },
                        new
                        {
                            PermissionId = 101,
                            RoleId = new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb")
                        },
                        new
                        {
                            PermissionId = 201,
                            RoleId = new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb")
                        },
                        new
                        {
                            PermissionId = 101,
                            RoleId = new Guid("78114707-a61a-46e7-9914-f918de3f89fa")
                        },
                        new
                        {
                            PermissionId = 201,
                            RoleId = new Guid("78114707-a61a-46e7-9914-f918de3f89fa")
                        },
                        new
                        {
                            PermissionId = 202,
                            RoleId = new Guid("78114707-a61a-46e7-9914-f918de3f89fa")
                        },
                        new
                        {
                            PermissionId = 101,
                            RoleId = new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230")
                        },
                        new
                        {
                            PermissionId = 301,
                            RoleId = new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230")
                        },
                        new
                        {
                            PermissionId = 101,
                            RoleId = new Guid("dcd622b3-400c-45e2-9451-23af77b7f835")
                        },
                        new
                        {
                            PermissionId = 301,
                            RoleId = new Guid("dcd622b3-400c-45e2-9451-23af77b7f835")
                        },
                        new
                        {
                            PermissionId = 302,
                            RoleId = new Guid("dcd622b3-400c-45e2-9451-23af77b7f835")
                        });
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SessionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.ApplicationPermission", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", null)
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Client", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Common.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Tiebreaker.Domain.Models.Common.ContactInfo", "ContactInfo")
                        .WithMany()
                        .HasForeignKey("ContactInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Group", "Group")
                        .WithMany("Clients")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Common.Name", "Name")
                        .WithMany()
                        .HasForeignKey("NameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("ContactInfo");

                    b.Navigation("Group");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.Email", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Common.ContactInfo", null)
                        .WithMany("Emails")
                        .HasForeignKey("ContactInfoId");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.PhoneNumber", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Common.ContactInfo", null)
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("ContactInfoId");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupApplication", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", null)
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserApplication", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.GroupUser", "GroupUser")
                        .WithMany("Applications")
                        .HasForeignKey("GroupUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("GroupUser");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserApplicationRole", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.GroupUserApplication", null)
                        .WithMany()
                        .HasForeignKey("GroupUserApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserRole", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.GroupUser", null)
                        .WithMany()
                        .HasForeignKey("GroupUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Role", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("Tiebreaker.Domain.Models.Group", "Group")
                        .WithMany("Roles")
                        .HasForeignKey("GroupId");

                    b.Navigation("Application");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.RolePermission", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Session", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Client", null)
                        .WithMany("Sessions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Tag", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Session", "Session")
                        .WithMany("Tags")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Client", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Common.ContactInfo", b =>
                {
                    b.Navigation("Emails");

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Group", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("GroupUsers");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Session", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
