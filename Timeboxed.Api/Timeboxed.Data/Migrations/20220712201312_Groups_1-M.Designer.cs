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
    [Migration("20220712201312_Groups_1-M")]
    partial class Groups_1M
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
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"), 1L, 1);

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

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.HasIndex("AuditId");

                    b.HasIndex("GroupId");

                    b.HasIndex("PermissionId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Audit", b =>
                {
                    b.Property<Guid>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuditId");

                    b.HasIndex("User");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.HasIndex("AuditId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.Property<Guid>("GroupUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupUserId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"), 1L, 1);

                    b.Property<Guid>("AuditId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PermissionId");

                    b.HasIndex("AuditId");

                    b.HasIndex("GroupUserId");

                    b.HasIndex("UserApplicationId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DefaultGroupGroupId")
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

                    b.HasKey("UserId");

                    b.HasIndex("DefaultGroupGroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.UserApplication", b =>
                {
                    b.Property<Guid>("UserApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<Guid?>("GroupUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserApplicationId");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("GroupUserId");

                    b.ToTable("UserApplications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Application", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Audit", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.Group", null)
                        .WithMany("Applications")
                        .HasForeignKey("GroupId");

                    b.HasOne("Tiebreaker.Domain.Models.Permission", null)
                        .WithMany("Applications")
                        .HasForeignKey("PermissionId");

                    b.Navigation("Audit");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Audit", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Group", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Audit", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Audit");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Group", null)
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId");

                    b.HasOne("Tiebreaker.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Permission", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Audit", "Audit")
                        .WithMany()
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.GroupUser", null)
                        .WithMany("Permissions")
                        .HasForeignKey("GroupUserId");

                    b.HasOne("Tiebreaker.Domain.Models.UserApplication", null)
                        .WithMany("Permissions")
                        .HasForeignKey("UserApplicationId");

                    b.Navigation("Audit");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.User", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Group", "DefaultGroup")
                        .WithMany()
                        .HasForeignKey("DefaultGroupGroupId");

                    b.Navigation("DefaultGroup");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.UserApplication", b =>
                {
                    b.HasOne("Tiebreaker.Domain.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tiebreaker.Domain.Models.GroupUser", null)
                        .WithMany("UserApplications")
                        .HasForeignKey("GroupUserId");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Group", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("GroupUsers");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.GroupUser", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("UserApplications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.Permission", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("Tiebreaker.Domain.Models.UserApplication", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
