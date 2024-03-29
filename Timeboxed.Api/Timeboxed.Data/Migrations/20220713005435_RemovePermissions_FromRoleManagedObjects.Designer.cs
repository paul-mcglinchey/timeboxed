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
    [Migration("20220713005435_RemovePermissions_FromRoleManagedObjects")]
    partial class RemovePermissions_FromRoleManagedObjects
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
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.ApplicationRole", b =>
                {
                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("ApplicationRoles");
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
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
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

            modelBuilder.Entity("Tiebreaker.Domain.Models.ApplicationRole", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", null)
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Tiebreaker.Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUserApplication", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.GroupUser", "GroupUser")
                        .WithMany()
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
#pragma warning restore 612, 618
        }
    }
}
