﻿// <auto-generated />
using System;
using CSharpFinalProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSharpFinalProject.Migrations
{
    [DbContext(typeof(AttendanceDbContext))]
    partial class AttendanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentAttendanceSystem.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("StudentAttendanceSystem.ClassSchedule", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TotalClasses")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.ToTable("ClassSchedules");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Fees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentAttendanceSystem.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StudentAttendanceSystem.Admin", b =>
                {
                    b.HasBaseType("StudentAttendanceSystem.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Name = "admin",
                            Password = "123456",
                            Username = "admin"
                        },
                        new
                        {
                            Id = -2,
                            Name = "Eakub",
                            Password = "123456",
                            Username = "eakub"
                        });
                });

            modelBuilder.Entity("StudentAttendanceSystem.Student", b =>
                {
                    b.HasBaseType("StudentAttendanceSystem.User");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.HasIndex("CourseId");

                    b.ToTable("Users", t =>
                        {
                            t.Property("CourseId")
                                .HasColumnName("Student_CourseId");
                        });

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Teacher", b =>
                {
                    b.HasBaseType("StudentAttendanceSystem.User");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.HasIndex("CourseId");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Attendance", b =>
                {
                    b.HasOne("StudentAttendanceSystem.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentAttendanceSystem.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentAttendanceSystem.ClassSchedule", b =>
                {
                    b.HasOne("StudentAttendanceSystem.Course", "Course")
                        .WithOne("ClassSchedule")
                        .HasForeignKey("StudentAttendanceSystem.ClassSchedule", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Course", b =>
                {
                    b.HasOne("StudentAttendanceSystem.User", null)
                        .WithMany("Courses")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Student", b =>
                {
                    b.HasOne("StudentAttendanceSystem.Course", null)
                        .WithMany("Students")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Teacher", b =>
                {
                    b.HasOne("StudentAttendanceSystem.Course", null)
                        .WithMany("Teachers")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("StudentAttendanceSystem.Course", b =>
                {
                    b.Navigation("ClassSchedule")
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("StudentAttendanceSystem.User", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
