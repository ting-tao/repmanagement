﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepManagement.Models;

namespace RepManagement.Models.Migrations
{
    [DbContext(typeof(RepManagementContext))]
    [Migration("20181106014819_update-20181106")]
    partial class update20181106
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RepManagement.Models.Dictionary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int>("DicTypeID");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Dictionaries");
                });

            modelBuilder.Entity("RepManagement.Models.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("ColorNum");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid?>("IconID");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<double?>("LeftCount");

                    b.Property<string>("MaterialQuality");

                    b.Property<string>("OpenSize");

                    b.Property<decimal?>("Price");

                    b.Property<string>("SerialNum");

                    b.Property<string>("Spec");

                    b.Property<Guid>("TypeID");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid>("VendorID");

                    b.Property<double?>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("RepManagement.Models.Mould", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BackImgID");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Creator");

                    b.Property<Guid?>("FrontImgID");

                    b.Property<double?>("Height");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("RelateMaterial");

                    b.Property<string>("SerialNum");

                    b.Property<Guid?>("SideImgID");

                    b.Property<double?>("Size");

                    b.Property<string>("Spec");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<double?>("Width");

                    b.HasKey("Id");

                    b.ToTable("Moulds");
                });

            modelBuilder.Entity("RepManagement.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("RoleName");

                    b.Property<int>("RoleType");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RepManagement.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DisplayName")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepManagement.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<Guid>("RoleID");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<Guid>("UserID");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("RepManagement.Models.Vendor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ContactPhone");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerEval");

                    b.Property<double?>("Deadline");

                    b.Property<string>("FactoryEval");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Level");

                    b.Property<string>("ManagerName");

                    b.Property<string>("ManagerPhone");

                    b.Property<double?>("Production");

                    b.Property<string>("SerialNum");

                    b.Property<string>("Style");

                    b.Property<Guid>("TypeID");

                    b.Property<Guid>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("VendorName");

                    b.Property<double?>("Years");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });
#pragma warning restore 612, 618
        }
    }
}
