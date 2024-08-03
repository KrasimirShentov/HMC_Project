﻿// <auto-generated />
using System;
using HMC_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HMC_Project.Migrations
{
    [DbContext(typeof(HMCDbContext))]
    [Migration("20240803065018_CreatingDatabase")]
    partial class CreatingDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HMC_Project.Models.Address", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HMC_Project.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressID")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HMC_Project.Models.DepartmentAddress", b =>
                {
                    b.Property<Guid>("DepartmentID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressID")
                        .HasColumnType("uuid");

                    b.HasKey("DepartmentID", "AddressID");

                    b.HasIndex("AddressID");

                    b.ToTable("DepartmentAddresses");
                });

            modelBuilder.Entity("HMC_Project.Models.Employee", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressID")
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DepartmentId1")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TrainingId")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DepartmentId1");

                    b.HasIndex("TrainingId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HMC_Project.Models.EmployeeAddress", b =>
                {
                    b.Property<Guid>("EmployeeID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressID")
                        .HasColumnType("uuid");

                    b.HasKey("EmployeeID", "AddressID");

                    b.HasIndex("AddressID");

                    b.ToTable("EmployeeAddress");
                });

            modelBuilder.Entity("HMC_Project.Models.Training", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TrainingHours")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Training");

                    b.HasData(
                        new
                        {
                            ID = new Guid("d6bdccc2-6542-4c82-b8ae-1b4d6560516e"),
                            Description = "Description1",
                            PositionName = "Position1",
                            TrainingHours = 40,
                            Type = "Type1"
                        });
                });

            modelBuilder.Entity("HMC_Project.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HMC_Project.Models.UserAddress", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserAddressID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("UserID", "UserAddressID");

                    b.HasIndex("AddressID");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("HMC_Project.Models.Department", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", null)
                        .WithMany("DepartmentAddresses")
                        .HasForeignKey("AddressID");
                });

            modelBuilder.Entity("HMC_Project.Models.DepartmentAddress", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMC_Project.Models.Department", "Department")
                        .WithMany("DepartmentAddresses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("HMC_Project.Models.Employee", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", null)
                        .WithMany("EmployeeAddresses")
                        .HasForeignKey("AddressID");

                    b.HasOne("HMC_Project.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMC_Project.Models.Department", null)
                        .WithMany("employees")
                        .HasForeignKey("DepartmentId1");

                    b.HasOne("HMC_Project.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("HMC_Project.Models.EmployeeAddress", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMC_Project.Models.Employee", "Employee")
                        .WithMany("EmployeeAddresses")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HMC_Project.Models.User", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", null)
                        .WithMany("UserAddresses")
                        .HasForeignKey("AddressID");
                });

            modelBuilder.Entity("HMC_Project.Models.UserAddress", b =>
                {
                    b.HasOne("HMC_Project.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HMC_Project.Models.User", "User")
                        .WithMany("UserAddresses")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HMC_Project.Models.Address", b =>
                {
                    b.Navigation("DepartmentAddresses");

                    b.Navigation("EmployeeAddresses");

                    b.Navigation("UserAddresses");
                });

            modelBuilder.Entity("HMC_Project.Models.Department", b =>
                {
                    b.Navigation("DepartmentAddresses");

                    b.Navigation("employees");
                });

            modelBuilder.Entity("HMC_Project.Models.Employee", b =>
                {
                    b.Navigation("EmployeeAddresses");
                });

            modelBuilder.Entity("HMC_Project.Models.User", b =>
                {
                    b.Navigation("UserAddresses");
                });
#pragma warning restore 612, 618
        }
    }
}